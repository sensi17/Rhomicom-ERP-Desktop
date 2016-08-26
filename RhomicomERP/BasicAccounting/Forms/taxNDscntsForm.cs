using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;

namespace Accounting.Forms
{
  public partial class taxNDscntsForm : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";
    bool addRec = false;
    bool editRec = false;
    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;

    #endregion

    #region "FORM EVENTS..."
    public taxNDscntsForm()
    {
      InitializeComponent();
    }

    private void taxNDscntsForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel3.TopFill = clrs[0];
      this.glsLabel3.BottomFill = clrs[1];
    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]);

      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;
      this.vwSQLButton.Enabled = vwSQL;
      this.rcHstryButton.Enabled = rcHstry;
    }
    #endregion

    #region "TAX CODES..."
    public void loadPanel()
    {
      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      if (this.searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
        || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_rec = false;
      this.totl_rec = Global.mnFrm.cmCde.Big_Val;
      this.getPnlData();
      this.obey_evnts = true;
    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populateListVw();
      this.updtNavLabels();
    }

    private void updtTotals()
    {
      Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
        long.Parse(this.dsplySizeComboBox.Text), this.totl_rec);
      if (this.rec_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
      {
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      if (this.rec_cur_indx < 0)
      {
        this.rec_cur_indx = 0;
      }
      Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.rec_cur_indx;
    }

    private void updtNavLabels()
    {
      this.moveFirstButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_rec == true ||
        this.totl_rec != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecsLabel.Text = "of Total";
      }
    }

    private void populateListVw()
    {
      this.obey_evnts = false;
      DataSet dtst = Global.get_Basic_Tax(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id);
      this.taxListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()});
        this.taxListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.taxListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.taxListView.Items[0].Selected = true;
      }
      else
      {
        this.populateDet(-10000);
      }
      this.obey_evnts = true;
    }

    private void populateDet(int codeID)
    {
      if (this.editRec == false)
      {
        this.clearDetInfo();
        this.disableDetEdit();
      }
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_TaxDet(codeID);

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.itmIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();//this.taxListView.SelectedItems[0].SubItems[2].Text;
        this.itmNameTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();//this.taxListView.SelectedItems[0].SubItems[1].Text;
        this.descTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();//this.taxListView.SelectedItems[0].SubItems[3].Text;
        this.isEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          dtst.Tables[0].Rows[i][4].ToString());

        this.isParentCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
  dtst.Tables[0].Rows[i][14].ToString());

        this.checkParent();

        this.rcvrblTaxCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          dtst.Tables[0].Rows[i][9].ToString());
        this.wthdngTaxCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          dtst.Tables[0].Rows[i][10].ToString());

        this.sqlFrmlrTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();//this.taxListView.SelectedItems[0].SubItems[11].Text;

        this.taxPyblAcntIDTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();//this.taxListView.SelectedItems[0].SubItems[6].Text;
        this.taxPyblAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][5].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][5].ToString()));

        this.expnsAcntIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
        this.expnsAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][6].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][6].ToString()));

        this.rvnuAcntIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        this.rvnuAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][7].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][7].ToString()));

        this.taxExpnsAccntIDTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();//this.taxListView.SelectedItems[0].SubItems[6].Text;
        this.taxExpnseAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][11].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][11].ToString()));

        this.prchsDscntAccntIDTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();
        this.prchsDscntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][12].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][12].ToString()));

        this.chrgeExpnsAccntIDTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
        this.chrgeExpnsAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][13].ToString())) +
            "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][13].ToString()));

        this.childItemsListView.Items.Clear();
        char[] w = { ',' };
        string[] codeIDs = dtst.Tables[0].Rows[i][15].ToString().Split(w, StringSplitOptions.RemoveEmptyEntries);

        for (int j = 0; j < codeIDs.Length; j++)
        {
          if (int.Parse(codeIDs[j]) > 0)
          {
            ListViewItem nwItem = new ListViewItem(new string[] {
    (j+1).ToString(),
    Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes","code_id","code_name",int.Parse(codeIDs[j])),
    codeIDs[j]});
            this.childItemsListView.Items.Add(nwItem);
          }
        }
        if (Global.isTaxItmInUse(int.Parse(this.itmIDTextBox.Text)) == false
          && this.editRec == true)
        {
          this.sqlFrmlrTextBox.ReadOnly = false;
          this.sqlFrmlrTextBox.BackColor = Color.FromArgb(255, 255, 128);
          string selItm = this.itmTypeComboBox.Text;
          this.itmTypeComboBox.Items.Clear();
          this.itmTypeComboBox.Items.Add("Tax");
          this.itmTypeComboBox.Items.Add("Discount");
          this.itmTypeComboBox.Items.Add("Extra Charge");
          //if (this.editRec == true)
          //{
          this.itmTypeComboBox.SelectedItem = selItm;
          //}
        }
        else
        {
          this.sqlFrmlrTextBox.ReadOnly = true;
          this.sqlFrmlrTextBox.BackColor = Color.WhiteSmoke;
          this.itmTypeComboBox.Items.Clear();
          this.itmTypeComboBox.Items.Add(dtst.Tables[0].Rows[i][3].ToString());
          this.itmTypeComboBox.SelectedItem = dtst.Tables[0].Rows[i][3].ToString();//;

          //string selItm = this.itmTypeComboBox.Text;
          //this.itmTypeComboBox.Items.Clear();
          //this.itmTypeComboBox.Items.Add(selItm);
          ////if (this.editRec == true)
          ////{
          //this.itmTypeComboBox.SelectedItem = selItm;
          //}
        }
      }
      this.obey_evnts = true;
    }

    private void correctNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.rec_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_rec = true;
        this.totl_rec = 0;
        this.last_rec_num = 0;
        this.rec_cur_indx = 0;
        this.updtTotals();
        this.updtNavLabels();
      }
      else if (this.totl_rec == Global.mnFrm.cmCde.Big_Val
     && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_rec = this.last_rec_num;
        if (totlRecs == 0)
        {
          this.rec_cur_indx -= 1;
          this.updtTotals();
          this.populateListVw();
        }
        else
        {
          this.updtTotals();
        }
      }
    }

    private bool shdObeyEvts()
    {
      return this.obey_evnts;
    }

    private void PnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_rec = true;
        this.totl_rec = Global.get_Total_Tax(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();
    }

    private void clearDetInfo()
    {
      this.obey_evnts = false;
      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;
      this.itmIDTextBox.Text = "-1";
      this.itmNameTextBox.Text = "";
      this.itmTypeComboBox.Items.Clear();
      this.descTextBox.Text = "";

      this.taxPyblAcntIDTextBox.Text = "-1";
      this.taxPyblAcntNmTextBox.Text = "";

      this.expnsAcntIDTextBox.Text = "-1";
      this.expnsAcntNmTextBox.Text = "";

      this.rvnuAcntIDTextBox.Text = "-1";
      this.rvnuAcntNmTextBox.Text = "";

      this.taxExpnsAccntIDTextBox.Text = "-1";
      this.taxExpnseAccntTextBox.Text = "";

      this.prchsDscntAccntIDTextBox.Text = "-1";
      this.prchsDscntTextBox.Text = "";

      this.chrgeExpnsAccntIDTextBox.Text = "-1";
      this.chrgeExpnsAccntTextBox.Text = "";

      this.sqlFrmlrTextBox.Text = "";
      this.isEnbldCheckBox.Checked = false;
      this.rcvrblTaxCheckBox.Checked = false;
      this.wthdngTaxCheckBox.Checked = false;

      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      this.saveButton.Enabled = true;
      this.itmNameTextBox.ReadOnly = false;
      this.itmNameTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.descTextBox.ReadOnly = false;
      this.descTextBox.BackColor = Color.White;
      this.sqlFrmlrTextBox.ReadOnly = false;
      this.sqlFrmlrTextBox.BackColor = Color.FromArgb(255, 255, 128);

      if (Global.isTaxItmInUse(int.Parse(this.itmIDTextBox.Text)) == false)
      {
        string selItm = this.itmTypeComboBox.Text;
        this.itmTypeComboBox.Items.Clear();
        this.itmTypeComboBox.Items.Add("Tax");
        this.itmTypeComboBox.Items.Add("Discount");
        this.itmTypeComboBox.Items.Add("Extra Charge");
        //if (this.editRec == true)
        //{
        this.itmTypeComboBox.SelectedItem = selItm;
        //}
      }
      else
      {
        //this.sqlFrmlrTextBox.ReadOnly = true;
        //this.sqlFrmlrTextBox.BackColor = Color.WhiteSmoke;

        //string selItm = this.itmTypeComboBox.Text;
        //this.itmTypeComboBox.Items.Clear();
        //this.itmTypeComboBox.Items.Add(selItm);
        ////if (this.editRec == true)
        ////{
        //this.itmTypeComboBox.SelectedItem = selItm;
        ////}
      }
    }

    private void disableDetEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.saveButton.Enabled = false;
      this.editButton.Text = "EDIT";
      this.itmNameTextBox.ReadOnly = true;
      this.itmNameTextBox.BackColor = Color.WhiteSmoke;
      this.descTextBox.ReadOnly = true;
      this.descTextBox.BackColor = Color.WhiteSmoke;
      this.sqlFrmlrTextBox.ReadOnly = true;
      this.sqlFrmlrTextBox.BackColor = Color.WhiteSmoke;

      this.taxPyblAcntNmTextBox.ReadOnly = true;
      this.taxPyblAcntNmTextBox.BackColor = Color.WhiteSmoke;

      this.expnsAcntNmTextBox.ReadOnly = true;
      this.expnsAcntNmTextBox.BackColor = Color.WhiteSmoke;

      this.rvnuAcntNmTextBox.ReadOnly = true;
      this.rvnuAcntNmTextBox.BackColor = Color.WhiteSmoke;

      this.taxExpnseAccntTextBox.ReadOnly = true;
      this.taxExpnseAccntTextBox.BackColor = Color.WhiteSmoke;

      this.prchsDscntTextBox.ReadOnly = true;
      this.prchsDscntTextBox.BackColor = Color.WhiteSmoke;

      this.chrgeExpnsAccntTextBox.ReadOnly = true;
      this.chrgeExpnsAccntTextBox.BackColor = Color.WhiteSmoke;

    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton_Click(this.goButton, ex);
      }
    }

    private void positionTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.PnlNavButtons(this.movePreviousButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.PnlNavButtons(this.moveNextButton, ex);
      }
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void taxListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.taxListView.SelectedItems.Count > 0)
      {
        this.populateDet(int.Parse(this.taxListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateDet(-100000);
      }
    }

    private void taxListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
      }
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearDetInfo();
      this.addRec = true;
      this.editRec = false;
      this.prpareForDetEdit();
      this.sqlFrmlrTextBox.Text = "select 0.00 * {:unit_price}";
      this.addButton.Enabled = false;
      this.editButton.Enabled = false;
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.itmIDTextBox.Text == "" || this.itmIDTextBox.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForDetEdit();
        this.addButton.Enabled = false;
        this.editButton.Text = "STOP";
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableDetEdit();
        System.Windows.Forms.Application.DoEvents();
        this.loadPanel();
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == true)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[11]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[12]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      if (this.itmNameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter an Item name!", 0);
        return;
      }

      long oldRecID = Global.getChargeItmID(this.itmNameTextBox.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Item Name is already in use in this Organisation!", 0);
        return;
      }
      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.itmIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Item Name is already in use in this Organisation!", 0);
        return;
      }

      if (this.itmTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Item Type cannot be empty!", 0);
        return;
      }
      string chldCodeIDs = ",";
      for (int c = 0; c < this.childItemsListView.Items.Count; c++)
      {
        chldCodeIDs = chldCodeIDs + this.childItemsListView.Items[c].SubItems[2].Text + ",";
      }

      if (this.isParentCheckBox.Checked == false)
      {
        if (this.itmTypeComboBox.Text == "Tax" &&
          (this.taxPyblAcntIDTextBox.Text == ""
          || this.taxPyblAcntIDTextBox.Text == "-1"))
        {
          Global.mnFrm.cmCde.showMsg("Taxes Payable Account CANNOT be EMPTY if Item Type is Tax!", 0);
          return;
        }
        if (this.itmTypeComboBox.Text != "Tax" &&
          (this.taxPyblAcntIDTextBox.Text != ""
          && this.taxPyblAcntIDTextBox.Text != "-1"))
        {
          Global.mnFrm.cmCde.showMsg("Taxes Payable Account MUST be EMPTY if Item Type is not Tax!", 0);
          return;
        }

        if (this.itmTypeComboBox.Text == "Discount" && (this.expnsAcntIDTextBox.Text == ""
          || this.expnsAcntIDTextBox.Text == "-1"))
        {
          Global.mnFrm.cmCde.showMsg("Contra Revenue Account CANNOT be EMPTY if Item Type is Discount!", 0);
          return;
        }
        if (this.itmTypeComboBox.Text != "Discount" && (this.expnsAcntIDTextBox.Text != ""
    && this.expnsAcntIDTextBox.Text != "-1"))
        {
          Global.mnFrm.cmCde.showMsg("Contra Revenue Account MUST be EMPTY if Item Type is not Discount!", 0);
          return;
        }

        if (this.itmTypeComboBox.Text == "Extra Charge" && (this.rvnuAcntIDTextBox.Text == ""
          || this.rvnuAcntIDTextBox.Text == "-1"))
        {
          Global.mnFrm.cmCde.showMsg("Revenue Account CANNOT be EMPTY if Item Type is Extra Charge!", 0);
          return;
        }
        if (this.itmTypeComboBox.Text != "Extra Charge" && (this.rvnuAcntIDTextBox.Text != ""
    && this.rvnuAcntIDTextBox.Text != "-1"))
        {
          Global.mnFrm.cmCde.showMsg("Revenue Account MUST be EMPTY if Item Type is not Extra Charge!", 0);
          return;
        }
      }
      else if (this.childItemsListView.Items.Count <= 0) //&& this.editRec == true
      {
        Global.mnFrm.cmCde.showMsg("Child Items cannot be empty for a Parent Item!", 0);
        return;
      }
      if (!this.isSQLValid())
      {
        return;
      }
      if (this.addRec == true)
      {
        Global.createTaxRec(Global.mnFrm.cmCde.Org_id, this.itmNameTextBox.Text,
          this.descTextBox.Text, this.itmTypeComboBox.Text, this.isEnbldCheckBox.Checked,
          int.Parse(this.taxPyblAcntIDTextBox.Text), int.Parse(this.expnsAcntIDTextBox.Text),
          int.Parse(this.rvnuAcntIDTextBox.Text), this.sqlFrmlrTextBox.Text,
          this.rcvrblTaxCheckBox.Checked, int.Parse(this.taxExpnsAccntIDTextBox.Text),
          int.Parse(this.prchsDscntAccntIDTextBox.Text), int.Parse(this.chrgeExpnsAccntIDTextBox.Text),
          this.wthdngTaxCheckBox.Checked, this.isParentCheckBox.Checked, chldCodeIDs);

        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.addRecsP;
        this.addButton.Enabled = this.editRecsP;

        Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        System.Windows.Forms.Application.DoEvents();
        this.loadPanel();
      }
      else if (this.editRec == true)
      {
        Global.updateTaxRec(int.Parse(this.itmIDTextBox.Text), this.itmNameTextBox.Text,
          this.descTextBox.Text, this.itmTypeComboBox.Text, this.isEnbldCheckBox.Checked,
          int.Parse(this.taxPyblAcntIDTextBox.Text), int.Parse(this.expnsAcntIDTextBox.Text),
          int.Parse(this.rvnuAcntIDTextBox.Text), this.sqlFrmlrTextBox.Text,
          this.rcvrblTaxCheckBox.Checked, int.Parse(this.taxExpnsAccntIDTextBox.Text),
          int.Parse(this.prchsDscntAccntIDTextBox.Text), int.Parse(this.chrgeExpnsAccntIDTextBox.Text),
          this.wthdngTaxCheckBox.Checked, this.isParentCheckBox.Checked, chldCodeIDs);

        if (this.taxListView.SelectedItems.Count > 0)
        {
          this.taxListView.SelectedItems[0].SubItems[1].Text = this.itmNameTextBox.Text;
        }
        Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
      }
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[13]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.itmIDTextBox.Text == "" || this.itmIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Item to DELETE!", 0);
        return;
      }
      if (Global.isTaxItmInUse(int.Parse(this.itmIDTextBox.Text)) == true)
      {
        Global.mnFrm.cmCde.showMsg("This Item is in Use!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deleteTaxItm(int.Parse(this.itmIDTextBox.Text), this.itmNameTextBox.Text);
      this.loadPanel();
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 9);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.taxListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.taxListView.SelectedItems[0].SubItems[2].Text),
        "scm.scm_tax_codes", "code_id"), 10);
    }

    private void taxPyblAcntButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.taxPyblAcntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Liability Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.taxPyblAcntIDTextBox.Text = selVals[i];
          this.taxPyblAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
        }
      }
    }

    private void expnsAcntButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.expnsAcntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.expnsAcntIDTextBox.Text = selVals[i];
          this.expnsAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
        }
      }
    }

    private void rvnuAcntButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.rvnuAcntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Revenue Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.rvnuAcntIDTextBox.Text = selVals[i];
          this.rvnuAcntNmTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
        }
      }
    }

    private void isEnbldCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false
       || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.isEnbldCheckBox.Checked = !this.isEnbldCheckBox.Checked;
      }
    }

    private bool isSQLValid()
    {
      try
      {
        DataSet result = Global.mnFrm.cmCde.selectDataNoParams(
          this.sqlFrmlrTextBox.Text.Replace("{:unit_price}",
          this.unitPriceNumUpDown.Value.ToString()).Replace("{:qty}",
          this.qtyNumUpDown.Value.ToString()));
        long cnt = result.Tables[0].Rows.Count;
        if (cnt > 0)
        {
          return true;
        }
        else
        {
          Global.mnFrm.cmCde.showMsg("Invalid SQL Statement!\r\nQuery Returns No Results!", 0);
          return false;
        }
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg("SQL Statement is InValid\r\n" + ex.Message, 0);
        return false;
      }
    }

    private void testSQLButton_Click(object sender, EventArgs e)
    {
      if (this.isSQLValid())
      {
        Global.mnFrm.cmCde.showMsg("SQL Statement is Valid", 3);
      }
    }
    #endregion

    private void itmTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.itmTypeComboBox.Text == "Tax")
      {
        this.expnsAcntIDTextBox.Text = "-1";
        this.expnsAcntNmTextBox.Text = "";
        this.expnsAcntButton.Enabled = false;
        this.rvnuAcntIDTextBox.Text = "-1";
        this.rvnuAcntNmTextBox.Text = "";
        this.rvnuAcntButton.Enabled = false;
        this.taxPyblAcntButton.Enabled = true;
      }
      else if (this.itmTypeComboBox.Text == "Discount")
      {
        this.taxPyblAcntIDTextBox.Text = "-1";
        this.taxPyblAcntNmTextBox.Text = "";
        this.taxPyblAcntButton.Enabled = false;
        this.rvnuAcntIDTextBox.Text = "-1";
        this.rvnuAcntNmTextBox.Text = "";
        this.rvnuAcntButton.Enabled = false;
        this.expnsAcntButton.Enabled = true;
      }
      else if (this.itmTypeComboBox.Text == "Extra Charge")
      {
        this.taxPyblAcntIDTextBox.Text = "-1";
        this.taxPyblAcntNmTextBox.Text = "";
        this.taxPyblAcntButton.Enabled = false;
        this.expnsAcntIDTextBox.Text = "-1";
        this.expnsAcntNmTextBox.Text = "";
        this.expnsAcntButton.Enabled = false;
        this.rvnuAcntButton.Enabled = true;
      }
      if (this.addRec == true || editRec == true)
      {
        if (this.taxPyblAcntButton.Enabled == true)
        {
          this.taxPyblAcntNmTextBox.ReadOnly = false;
          this.taxPyblAcntNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
        }
        else
        {
          this.taxPyblAcntNmTextBox.ReadOnly = true;
          this.taxPyblAcntNmTextBox.BackColor = Color.WhiteSmoke;
        }
        if (this.expnsAcntButton.Enabled == true)
        {
          this.expnsAcntNmTextBox.ReadOnly = false;
          this.expnsAcntNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
        }
        else
        {
          this.expnsAcntNmTextBox.ReadOnly = true;
          this.expnsAcntNmTextBox.BackColor = Color.WhiteSmoke;
        }
        if (this.rvnuAcntButton.Enabled == true)
        {
          this.rvnuAcntNmTextBox.ReadOnly = false;
          this.rvnuAcntNmTextBox.BackColor = Color.FromArgb(255, 255, 128);
        }
        else
        {
          this.rvnuAcntNmTextBox.ReadOnly = true;
          this.rvnuAcntNmTextBox.BackColor = Color.WhiteSmoke;
        }
        if (this.taxExpnseAccntButton.Enabled == true)
        {
          this.taxExpnseAccntTextBox.ReadOnly = false;
          this.taxExpnseAccntTextBox.BackColor = Color.White;
        }
        else
        {
          this.taxExpnseAccntTextBox.ReadOnly = true;
          this.taxExpnseAccntTextBox.BackColor = Color.WhiteSmoke;
        }
        if (this.prchsDscntButton.Enabled == true)
        {
          this.prchsDscntTextBox.ReadOnly = false;
          this.prchsDscntTextBox.BackColor = Color.White;
        }
        else
        {
          this.prchsDscntTextBox.ReadOnly = true;
          this.prchsDscntTextBox.BackColor = Color.WhiteSmoke;
        }
        if (this.chrgeExpnsAccntButton.Enabled == true)
        {
          this.chrgeExpnsAccntTextBox.ReadOnly = false;
          this.chrgeExpnsAccntTextBox.BackColor = Color.White;
        }
        else
        {
          this.chrgeExpnsAccntTextBox.ReadOnly = true;
          this.chrgeExpnsAccntTextBox.BackColor = Color.WhiteSmoke;
        }
      }
    }

    private void rfrshButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void addMenuItem_Click(object sender, EventArgs e)
    {
      this.addButton_Click(this.addButton, e);
    }

    private void editMenuItem_Click(object sender, EventArgs e)
    {
      this.editButton_Click(this.editButton, e);
    }

    private void delMenuItem_Click(object sender, EventArgs e)
    {
      this.delButton_Click(this.delButton, e);
    }

    private void exptExMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.taxListView);
    }

    private void rfrshMenuItem_Click(object sender, EventArgs e)
    {
      this.goButton_Click(this.goButton, e);
    }

    private void vwSQLMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLButton_Click(this.vwSQLButton, e);
    }

    private void rcHstryMenuItem_Click(object sender, EventArgs e)
    {
      this.rcHstryButton_Click(this.rcHstryButton, e);
    }

    private void rcvrblTaxCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false
   || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.rcvrblTaxCheckBox.Checked = !this.rcvrblTaxCheckBox.Checked;
      }
    }

    private void wthdngTaxCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false
   || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.wthdngTaxCheckBox.Checked = !this.wthdngTaxCheckBox.Checked;
      }
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";
      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.disableDetEdit();
      this.rec_cur_indx = 0;
      this.rfrshButton_Click(this.rfrshButton, e);

    }

    private void taxNDscntsForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {
        if (this.saveButton.Enabled == true)
        {
          this.saveButton_Click(this.saveButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {
        if (this.addButton.Enabled == true)
        {
          this.addButton_Click(this.addButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {
        if (this.editButton.Enabled == true)
        {
          this.editButton_Click(this.editButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetButton.PerformClick();
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
      {
        if (this.goButton.Enabled == true)
        {
          this.goButton_Click(this.goButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {
        if (this.delButton.Enabled == true)
        {
          this.delButton_Click(this.delButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
        if (this.taxListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.taxListView, e);
        }
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void taxPyblAcntNmTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void taxPyblAcntNmTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "taxPyblAcntNmTextBox")
      {
        this.taxPyblAcntNmTextBox.Text = "";
        this.taxPyblAcntIDTextBox.Text = "-1";
        this.taxPyblAcntButton_Click(this.taxPyblAcntButton, e);
      }
      else if (mytxt.Name == "expnsAcntNmTextBox")
      {
        this.expnsAcntNmTextBox.Text = "";
        this.expnsAcntIDTextBox.Text = "-1";
        this.expnsAcntButton_Click(this.expnsAcntButton, e);
      }
      else if (mytxt.Name == "rvnuAcntNmTextBox")
      {
        this.rvnuAcntNmTextBox.Text = "";
        this.rvnuAcntIDTextBox.Text = "-1";
        this.rvnuAcntButton_Click(this.rvnuAcntButton, e);
      }
      else if (mytxt.Name == "taxExpnseAccntTextBox")
      {
        this.taxExpnseAccntTextBox.Text = "";
        this.taxExpnsAccntIDTextBox.Text = "-1";
        this.taxExpnseAccntButton_Click(this.taxExpnseAccntButton, e);
      }
      else if (mytxt.Name == "prchsDscntTextBox")
      {
        this.prchsDscntTextBox.Text = "";
        this.prchsDscntAccntIDTextBox.Text = "-1";
        this.prchsDscntButton_Click(this.prchsDscntButton, e);
      }
      else if (mytxt.Name == "chrgeExpnsAccntTextBox")
      {
        this.chrgeExpnsAccntTextBox.Text = "";
        this.chrgeExpnsAccntIDTextBox.Text = "-1";
        this.chrgeExpnsAccntButton_Click(this.chrgeExpnsAccntButton, e);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;

    }

    private void taxExpnseAccntButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.taxExpnsAccntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("All Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.taxExpnsAccntIDTextBox.Text = selVals[i];
          this.taxExpnseAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
        }
      }
    }

    private void prchsDscntButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.prchsDscntAccntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Contra Expense Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.prchsDscntAccntIDTextBox.Text = selVals[i];
          this.prchsDscntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
        }
      }
    }

    private void chrgeExpnsAccntButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.chrgeExpnsAccntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Expense Accounts"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id,
       this.srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.chrgeExpnsAccntIDTextBox.Text = selVals[i];
          this.chrgeExpnsAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(
            int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(
            int.Parse(selVals[i]));
        }
      }
    }

    private void addChildButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        this.editButton.PerformClick();
      }
      if (this.editRec == false && this.addRec == false)
      {
        //Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
        //    " this action!\nContact your System Administrator!", 0);
        return;
      }
      if ((this.itmIDTextBox.Text == "" ||
        this.itmIDTextBox.Text == "-1") && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Please select an Item First!", 0);
        return;
      }
      if (this.isParentCheckBox.Checked == false)
      {
        Global.mnFrm.cmCde.showMsg("This feature is valid for Parent Items Only!", 0);
        return;
      }
      string lovName = "";
      if (this.itmTypeComboBox.Text == "Tax")
      {
        lovName = "Tax Codes";
      }
      else if (this.itmTypeComboBox.Text == "Discount")
      {
        lovName = "Discount Codes";
      }
      else if (this.itmTypeComboBox.Text == "Extra Charge")
      {
        lovName = "Extra Charges";
      }
      string[] selVals = new string[1];
      for (int i = 0; i < this.childItemsListView.Items.Count; i++)
      {
        selVals[0] = this.childItemsListView.Items[i].SubItems[2].Text;
      }
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovName), ref selVals,
          false, false, Global.mnFrm.cmCde.Org_id, "0", "");
      if (dgRes == DialogResult.OK)
      {
        this.childItemsListView.Items.Clear();
        for (int i = 0; i < selVals.Length; i++)
        {
          if (int.Parse(selVals[i]) > 0)
          {
            ListViewItem nwItem = new ListViewItem(new string[] {
    (i+1).ToString(),
    Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes","code_id","code_name",int.Parse(selVals[i])),
    selVals[i]});
            this.childItemsListView.Items.Add(nwItem);
          }
        }
      }
    }

    private void deleteChildButton_Click(object sender, EventArgs e)
    {
      for (int i = 0; i < this.childItemsListView.SelectedItems.Count; )
      {
        this.childItemsListView.SelectedItems[0].Remove();
      }
    }

    private void isParentCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false
       || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.isParentCheckBox.Checked = !this.isParentCheckBox.Checked;
      }
      else
      {
        this.checkParent();
      }
    }

    private void checkParent()
    {
      if (this.isParentCheckBox.Checked)
      {
        this.accountsGroupBox.Enabled = false;
        this.childGroupBox.Enabled = true;
        this.sqlFrmlrTextBox.Text = "select 0";
        this.sqlFrmlrTextBox.Enabled = true;
        this.rcvrblTaxCheckBox.Enabled = false;
        this.wthdngTaxCheckBox.Enabled = false;

        this.rcvrblTaxCheckBox.Checked = false;
        this.wthdngTaxCheckBox.Checked = false;

        this.taxPyblAcntIDTextBox.Text = "-1";
        this.taxPyblAcntNmTextBox.Text = "";

        this.expnsAcntIDTextBox.Text = "-1";
        this.expnsAcntNmTextBox.Text = "";

        this.rvnuAcntIDTextBox.Text = "-1";
        this.rvnuAcntNmTextBox.Text = "";

        this.taxExpnsAccntIDTextBox.Text = "-1";
        this.taxExpnseAccntTextBox.Text = "";

        this.prchsDscntAccntIDTextBox.Text = "-1";
        this.prchsDscntTextBox.Text = "";

        this.chrgeExpnsAccntIDTextBox.Text = "-1";
        this.chrgeExpnsAccntTextBox.Text = "";
      }
      else
      {
        this.accountsGroupBox.Enabled = true;
        this.childGroupBox.Enabled = false;
        this.sqlFrmlrTextBox.Enabled = true;
        this.childItemsListView.Items.Clear();
      }
      System.Windows.Forms.Application.DoEvents();
      this.Refresh();
    }

    private void isParentCheckBox_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
      }
      else
      {
        this.checkParent();
      }
    }
  }
}