using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjectsManagement.Classes;
using ProjectsManagement.Forms;

namespace ProjectsManagement.Dialogs
{
  public partial class itmSearchDiag : Form
  {
    public itmSearchDiag()
    {
      InitializeComponent();
    }
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    //consgmtRcpt newRcpt = new consgmtRcpt();

    private long totl_vals = 0;
    private long cur_vals_idx = 0;
    private string vwSQLStmnt = "";
    private bool is_last_val = false;
    public bool cnsgmntsOnly = false;
    bool obeyEvnts = false;
    long last_vals_num = 0;
    public int my_org_id = Global.mnFrm.cmCde.Org_id;
    public string srchWrd = "%";
    public int storeid = -1;
    public int itmID = -1;
    public int srchIn = 0;
    public string itmNm = "";
    public string itmDesc = "";
    public double sellingPrc = 0.00;
    public double costPrc = 0.00;
    public string costPrcList = "";
    public string docType = "";
    public string cnsgmtIDs = "";
    public bool canLoad1stOne = false;
    public long cstmrSiteID = -1;
    public int taxID = -1;
    public int dscntID = -1;
    public int chrgeID = -1;
    public string taxNm = "";
    public string dscntNm = "";
    public string chrgeNm = "";
    public bool allcnsgmnts = false;
    public int chkdItmLstCnt = 0;
    public List<string[]> res;

    private void itmSearchDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      if (this.cnsgmntsOnly == true)
      {
        this.itmListView.Columns[8].Width = 0;
        this.itmListView.Columns[10].Width = 0;
        this.itmListView.Columns[11].Width = 0;
        this.itmListView.Columns[12].Width = 0;
        //this.itmListView.Columns[13].Width = 0;
        this.itmListView.Columns[14].Width = 0;
        this.itmListView.Columns[15].Width = 0;
        this.itmListView.Columns[16].Width = 0;
        this.itmListView.Columns[17].Width = 0;
        this.itmListView.Columns[18].Width = 0;
        this.itmListView.Columns[19].Width = 0;
        this.itmListView.Columns[20].Width = 0;
        this.itmListView.Columns[21].Width = 0;
        this.itmListView.Columns[22].Width = 0;
        this.itmListView.CheckBoxes = true;
      }
      else
      {
        this.itmListView.CheckBoxes = true;
        this.itmListView.Columns[3].Width = 0;
        this.itmListView.Columns[7].Width = 0;
        this.itmListView.Columns[9].Width = 0;
      }
      System.Windows.Forms.Application.DoEvents();
      this.loadValPanel();
      this.canLoad1stOne = false;
      this.searchForTextBox.Focus();
      System.Windows.Forms.Application.DoEvents();
    }

    private void loadValPanel()
    {
      this.obeyEvnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = srchIn;
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
       || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = "10";// Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.searchForTextBox.Text == "")
      {
        this.searchForTextBox.Text = this.srchWrd;
      }
      if (searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }

      this.is_last_val = false;
      this.totl_vals = Global.mnFrm.cmCde.Big_Val;
      this.cur_vals_idx = 0;
      this.getValPnlData();
      this.obeyEvnts = true;
    }

    private void getValPnlData()
    {
      this.updtValTotals();
      this.populateValGridVw();
      this.updtValNavLabels();
    }

    private void updtValTotals()
    {
      this.myNav.FindNavigationIndices(int.Parse(this.dsplySizeComboBox.Text),
      this.totl_vals);

      if (this.cur_vals_idx >= this.myNav.totalGroups)
      {
        this.cur_vals_idx = this.myNav.totalGroups - 1;
      }
      if (this.cur_vals_idx < 0)
      {
        this.cur_vals_idx = 0;
      }
      this.myNav.currentNavigationIndex = this.cur_vals_idx;
    }

    private void updtValNavLabels()
    {
      this.moveFirstButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_val == true ||
        this.totl_vals != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecLabel.Text = "of Total";
      }
    }

    private void populateValGridVw()
    {
      this.obeyEvnts = false;
      DataSet dtst;
      string dateStr = DateTime.ParseExact(
Global.mnFrm.cmCde.getDB_Date_time(), "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");

      if (this.allcnsgmnts == true && this.cnsgmntsOnly == true)
      {
        dtst = Global.get_AllConsignments(this.searchForTextBox.Text,
      this.searchInComboBox.Text, this.cur_vals_idx,
      int.Parse(this.dsplySizeComboBox.Text), this.my_org_id, this.cstmrSiteID);
      }
      else
      {
        dtst = Global.get_StoreItems(this.searchForTextBox.Text,
      this.searchInComboBox.Text, this.cur_vals_idx,
      int.Parse(this.dsplySizeComboBox.Text), this.my_org_id,
      this.storeid, this.docType, this.cnsgmntsOnly, this.itmID, this.cstmrSiteID);
      }

      this.itmListView.Items.Clear();
      if (this.cnsgmntsOnly == true)
      {
        //this.itmListView.Columns[5].Width = 0;
        //this.itmListView.Columns[6].Width = 0;
        this.itmListView.Columns[8].Width = 0;
        this.itmListView.Columns[10].Width = 0;
        this.itmListView.Columns[11].Width = 0;
        this.itmListView.Columns[12].Width = 0;
        //this.itmListView.Columns[13].Width = 0;
        this.itmListView.Columns[14].Width = 0;
        this.itmListView.Columns[15].Width = 0;
        this.itmListView.Columns[16].Width = 0;
        this.itmListView.Columns[17].Width = 0;
        this.itmListView.Columns[18].Width = 0;
        this.itmListView.Columns[19].Width = 0;
        this.itmListView.Columns[20].Width = 0;
        this.itmListView.Columns[21].Width = 0;
        this.itmListView.Columns[22].Width = 0;
      }
      else
      {
        this.itmListView.Columns[3].Width = 0;
        //this.itmListView.Columns[5].Width = 0;
        //this.itmListView.Columns[6].Width = 0;
        this.itmListView.Columns[7].Width = 0;
        this.itmListView.Columns[9].Width = 0;
      }
      System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        double avlbQty = 0;
        double rsvdQty = 0;
        double totQty = 0;
        long csgmntID = -1;
        double cstPrc = 0;
        string expDate = "";

        if (this.cnsgmntsOnly == true)
        {
          avlbQty = Global.getCsgmtLstAvlblBls(long.Parse(dtst.Tables[0].Rows[i][11].ToString()),
          dateStr);
          rsvdQty = Global.getCsgmtLstRsvdBls(long.Parse(dtst.Tables[0].Rows[i][11].ToString()),
           dateStr);
          totQty = Global.getCsgmtLstTotBls(long.Parse(dtst.Tables[0].Rows[i][11].ToString()),
          dateStr);
          csgmntID = long.Parse(dtst.Tables[0].Rows[i][11].ToString());
          cstPrc = double.Parse(dtst.Tables[0].Rows[i][12].ToString());
          expDate = DateTime.Parse(dtst.Tables[0].Rows[i][13].ToString()).ToString("dd-MMM-yyyy");
        }
        else
        {
          avlbQty = Global.getStockLstAvlblBls(long.Parse(dtst.Tables[0].Rows[i][5].ToString()),
     dateStr);
          rsvdQty = Global.getStockLstRsvdBls(long.Parse(dtst.Tables[0].Rows[i][5].ToString()),
          dateStr);
          totQty = Global.getStockLstTotBls(long.Parse(dtst.Tables[0].Rows[i][5].ToString()),
           dateStr);
        }
        this.last_vals_num = this.myNav.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
          dtst.Tables[0].Rows[i][2].ToString(),
          csgmntID.ToString(),
    avlbQty.ToString(),//.ToString("#,##0.00")
     rsvdQty.ToString(),//.ToString("#,##0.00")
    totQty.ToString(),//.ToString("#,##0.00")
    cstPrc.ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    expDate,
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_product_categories",
          "cat_id","cat_name",long.Parse(dtst.Tables[0].Rows[i][4].ToString())),
    Global.mnFrm.cmCde.getGnrlRecNm("inv.inv_itm_subinventories",
          "subinv_id","subinv_name",long.Parse(dtst.Tables[0].Rows[i][6].ToString())),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    Global.mnFrm.cmCde.getGnrlRecNm(
    "scm.scm_tax_codes", "code_id", "code_name", 
          int.Parse(dtst.Tables[0].Rows[i][8].ToString())),
    dtst.Tables[0].Rows[i][8].ToString(),
    Global.mnFrm.cmCde.getGnrlRecNm(
    "scm.scm_tax_codes", "code_id", "code_name", 
          int.Parse(dtst.Tables[0].Rows[i][9].ToString())),
    dtst.Tables[0].Rows[i][9].ToString(),
    Global.mnFrm.cmCde.getGnrlRecNm(
"scm.scm_tax_codes", "code_id", "code_name", 
          int.Parse(dtst.Tables[0].Rows[i][10].ToString())),
    dtst.Tables[0].Rows[i][10].ToString()});
        if (i % 2 == 1)
        {
          nwItem.BackColor = Color.WhiteSmoke;
          //nwItem.Font.b
        }
        if (this.cnsgmntsOnly == true &&
          ("," + this.cnsgmtIDs + ",").Contains("," +
          dtst.Tables[0].Rows[i][11].ToString() + ","))
        {
          nwItem.Checked = true;
        }
        if (this.cnsgmntsOnly == true)
        {
          if (avlbQty > 0)
          {
            nwItem.UseItemStyleForSubItems = false;
            nwItem.SubItems[4].BackColor = Color.Lime;
            nwItem.SubItems[13].BackColor = Color.Orange;

            this.itmListView.Items.Add(nwItem);
          }
        }
        else
        {
          nwItem.UseItemStyleForSubItems = false;
          if (avlbQty > 0)
          {
            nwItem.SubItems[4].BackColor = Color.Lime;
          }
          else
          {
            nwItem.SubItems[4].BackColor = Color.Red;
          }
          nwItem.SubItems[13].BackColor = Color.Orange;
          this.itmListView.Items.Add(nwItem);
        }
      }
      this.correctValsNavLbls(dtst);
      this.obeyEvnts = true;
      if (this.itmListView.Items.Count == 1
        && this.canLoad1stOne == true)
      {
        this.itmListView.Items[0].Selected = true;
        System.Windows.Forms.Application.DoEvents();
        this.okButton.PerformClick();
      }
      else
      {
        this.canLoad1stOne = false;
      }
      this.obeyEvnts = true;
    }

    private void correctValsNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.cur_vals_idx == 0 && totlRecs == 0)
      {
        this.is_last_val = true;
        this.totl_vals = 0;
        this.last_vals_num = 0;
        this.cur_vals_idx = 0;
        this.updtValTotals();
        this.updtValNavLabels();
      }
      else if (this.totl_vals == Global.mnFrm.cmCde.Big_Val
  && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_vals = this.last_vals_num;
        if (totlRecs == 0)
        {
          this.cur_vals_idx -= 1;
          this.updtValTotals();
          this.populateValGridVw();
        }
        else
        {
          this.updtValTotals();
        }
      }
    }

    private void valPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj =
        (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.cur_vals_idx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.cur_vals_idx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.cur_vals_idx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.totl_vals = Global.get_Total_StoreItms(
          this.searchForTextBox.Text, this.searchInComboBox.Text,
          this.my_org_id, this.storeid, this.docType, this.cnsgmntsOnly, this.itmID);
        this.is_last_val = true;
        this.updtValTotals();
        this.cur_vals_idx = this.myNav.totalGroups - 1;
      }
      this.getValPnlData();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (this.cnsgmntsOnly == true)
      {
        if (this.allcnsgmnts == true)
        {
          //Get list of all checked consignments and display in gridview
          res = new List<string[]>();
          this.chkdItmLstCnt = this.itmListView.CheckedItems.Count;

          foreach (ListViewItem chkdItm in itmListView.CheckedItems)
          {
            string[] testArry = new string[8];

            testArry[0] = chkdItm.SubItems[3].Text;
            testArry[1] = chkdItm.SubItems[1].Text;
            testArry[2] = chkdItm.SubItems[2].Text;
            testArry[3] = Global.getItmUOM(chkdItm.SubItems[1].Text);
            testArry[4] = chkdItm.SubItems[13].Text;
            testArry[5] = chkdItm.SubItems[6].Text;
            testArry[6] = chkdItm.SubItems[9].Text;
            testArry[7] = chkdItm.SubItems[7].Text;

            res.Add(testArry);
          }
        }
        else
        {
          this.cnsgmtIDs = ",";
          this.costPrcList = ",";
          for (int i = 0; i < this.itmListView.CheckedItems.Count; i++)
          {
            this.cnsgmtIDs += this.itmListView.CheckedItems[i].SubItems[3].Text + ",";
            this.costPrcList += this.itmListView.CheckedItems[i].SubItems[7].Text + ",";
          }
          this.cnsgmtIDs = this.cnsgmtIDs.Trim(',');
          this.costPrcList = this.costPrcList.Trim(',');
        }
      }
      else if (this.itmListView.CheckedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select an Item First!", 0);
        return;
      }
      else if (this.itmListView.CheckedItems.Count >= 1)
      {
        //this.itmID = int.Parse(e.Item.SubItems[10].Text);
        //this.storeid = int.Parse(e.Item.SubItems[15].Text);
        //this.itmNm = e.Item.SubItems[1].Text;
        //this.itmDesc = e.Item.SubItems[2].Text;
        //double.TryParse(e.Item.SubItems[8].Text, out this.sellingPrc);
        //int.TryParse(e.Item.SubItems[18].Text, out this.taxID);
        //int.TryParse(e.Item.SubItems[20].Text, out this.dscntID);
        //int.TryParse(e.Item.SubItems[22].Text, out this.chrgeID);
        //this.taxNm = e.Item.SubItems[17].Text;
        //this.dscntNm = e.Item.SubItems[19].Text;
        //this.chrgeNm = e.Item.SubItems[21].Text;

        res = new List<string[]>();
        this.chkdItmLstCnt = this.itmListView.CheckedItems.Count;

        foreach (ListViewItem chkdItm in itmListView.CheckedItems)
        {
          string[] testArry = new string[11];//Very very important to avoid same values entering List several times

          testArry[0] = chkdItm.SubItems[10].Text;//itmID
          testArry[1] = chkdItm.SubItems[15].Text;//storeid
          testArry[2] = chkdItm.SubItems[1].Text;//itmNm
          testArry[3] = chkdItm.SubItems[2].Text;//itmDesc
          testArry[4] = chkdItm.SubItems[8].Text;//sellingPrc
          testArry[5] = chkdItm.SubItems[18].Text;//taxID
          testArry[6] = chkdItm.SubItems[20].Text;//dscntID
          testArry[7] = chkdItm.SubItems[22].Text;//chrgeID
          testArry[8] = chkdItm.SubItems[17].Text;//taxNm
          testArry[9] = chkdItm.SubItems[19].Text;//dscntNm
          testArry[10] = chkdItm.SubItems[21].Text;//chrgeNm

          res.Add(testArry);
        }

      }
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void gotoButton_Click(object sender, EventArgs e)
    {
      this.loadValPanel();
    }

    private void exptExclTSrchMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.itmListView);
    }

    private void positionTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.valPnlNavButtons(this.movePreviousButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.valPnlNavButtons(this.moveNextButton, ex);
      }
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.gotoButton_Click(this.gotoButton, ex);
      }
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.itms_SQL, 10);
    }

    private void itmListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
    {
      if (this.obeyEvnts == false)
      {
        return;
      }
      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
        if (e.Item.Checked == false)
        {
          if (this.cnsgmntsOnly == false)
          {
            e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
            this.itmID = int.Parse(e.Item.SubItems[10].Text);
            this.storeid = int.Parse(e.Item.SubItems[15].Text);
            this.itmNm = e.Item.SubItems[1].Text;
            this.itmDesc = e.Item.SubItems[2].Text;
            double.TryParse(e.Item.SubItems[8].Text, out this.sellingPrc);
            int.TryParse(e.Item.SubItems[18].Text, out this.taxID);
            int.TryParse(e.Item.SubItems[20].Text, out this.dscntID);
            int.TryParse(e.Item.SubItems[22].Text, out this.chrgeID);
            this.taxNm = e.Item.SubItems[17].Text;
            this.dscntNm = e.Item.SubItems[19].Text;
            this.chrgeNm = e.Item.SubItems[21].Text;
            //double.TryParse(e.Item.SubItems[7].Text, out this.costPrc);
            e.Item.Checked = true;
          }
          else
          {
            e.Item.Checked = true;
          }
        }
        else
        {
          e.Item.Checked = false;
          this.itmID = -1;
          this.storeid = -1;
          this.itmNm = "";
          this.itmDesc = "";
          this.sellingPrc = 0.00;
          this.taxID = -1;
          this.dscntID = -1;
          this.chrgeID = -1;
          this.taxNm = "";
          this.dscntNm = "";
          this.chrgeNm = "";
          this.cnsgmtIDs = "";
        }
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
        this.itmID = -1;
        this.storeid = -1;
        this.itmNm = "";
        this.itmDesc = "";
        this.sellingPrc = 0.00;
        this.taxID = -1;
        this.dscntID = -1;
        this.chrgeID = -1;
        this.taxNm = "";
        this.dscntNm = "";
        this.chrgeNm = "";
        this.cnsgmtIDs = "";
      }
    }

    private void itmListView_DoubleClick(object sender, EventArgs e)
    {
      if (this.itmListView.SelectedItems.Count > 0)
      {
        this.itmListView.SelectedItems[0].Checked = true;
        //ListViewItemSelectionChangedEventArgs e1 = new ListViewItemSelectionChangedEventArgs(this.itmListView.SelectedItems[0], this.itmListView.SelectedIndices[0], true);
        //this.itmListView_ItemSelectionChanged(this.itmListView, e1);
        this.okButton_Click(this.okButton, e);
      }
    }

    private void itmSearchDiag_FormClosing(object sender, FormClosingEventArgs e)
    {
      //this.Dispose();      
      GC.Collect();
    }

    private void itmListView_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.okButton_Click(this.okButton, e);
      }
    }

    private void itmListView_Click(object sender, EventArgs e)
    {
      //if (this.itmListView.SelectedItems.Count > 0)
      //{
      //  ListViewItemSelectionChangedEventArgs e1 = new ListViewItemSelectionChangedEventArgs(this.itmListView.SelectedItems[0],
      //    this.itmListView.SelectedIndices[0], true);
      //  this.itmListView_ItemSelectionChanged(this.itmListView, e1);
      //  if (this.itmListView.CheckedItems.Count == 0)
      //  {
      //    this.itmListView.SelectedItems[0].Checked = true;
      //  }
      //}
    }


  }
}