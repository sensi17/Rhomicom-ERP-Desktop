using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;

namespace Accounting.Dialogs
{
  public partial class vwTrnsctnsDiag : Form
  {
    public vwTrnsctnsDiag()
    {
      InitializeComponent();
    }
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();

    private long totl_vals = 0;
    private long cur_vals_idx = 0;
    private string vwSQLStmnt = "";
    private bool is_last_val = false;
    bool obeyEvnts = false;
    public bool txtChngd = false;
    //public bool obeyEvnts = false;
    public string srchWrd = "";

    long last_vals_num = 0;
    public int my_org_id = 0;
    public string accnt_name = "";
    public string accnt_num = "";
    public int accntid = -1;
    public string dte1 = "";
    public string dte2 = "";
    public long trnsctnID = -1;

    private void vwTrnsctnsDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      System.Windows.Forms.Application.DoEvents();
      //this.searchInComboBox.SelectedIndex = -1;
      this.loadValPanel();
    }

    private void loadValPanel()
    {
      this.obeyEvnts = false;
      if (this.vldStrtDteTextBox.Text == "")
      {
        this.vldStrtDteTextBox.Text = this.dte1;
      }
      else
      {
        this.dte1 = this.vldStrtDteTextBox.Text;
      }
      if (this.vldEndDteTextBox.Text == "")
      {
        this.vldEndDteTextBox.Text = this.dte2;
      }
      else
      {
        this.dte2 = this.vldEndDteTextBox.Text;
      }
      if (this.orderByComboBox.SelectedIndex < 0)
      {
        this.orderByComboBox.SelectedIndex = 0;
      }
      
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        if (this.trnsctnID > 0)
        {
          this.searchInComboBox.SelectedIndex = 4;
        }
        else if (accnt_num == "")
        {
          this.searchInComboBox.SelectedIndex = 1;
        }
        else
        {
          this.searchInComboBox.SelectedIndex = 0;
        }
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
       || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }

      if (this.searchForTextBox.Text == "")
      {
        if (this.trnsctnID > 0)
        {
          this.searchForTextBox.Text = this.trnsctnID.ToString();
        }
        else if (accnt_num == "")
        {
          this.searchForTextBox.Text = this.accnt_name;
        }
        else
        {
          this.searchForTextBox.Text = this.accnt_num;
        }
      }
      if (this.searchForTextBox.Text.Contains("%") == false
        && (this.searchForTextBox.Text != this.accnt_num
        && this.searchForTextBox.Text != this.accnt_name
        && this.searchForTextBox.Text != this.trnsctnID.ToString()))
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }
      this.is_last_val = false;
      this.totl_vals = Global.mnFrm.cmCde.Big_Val;
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
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        this.obeyEvnts = true;
        return;
      }
      this.obeyEvnts = false;
      DataSet dtst;
      int ofst = 0;
      double ttlDbts = 0;
      double ttlCredits = 0;
      if (this.searchInComboBox.Text == "Transaction ID")
      {
        long trnsid = -1;
        long.TryParse(this.searchForTextBox.Text.Replace("%", ""), out trnsid);
        dtst = Global.get_IntfcTrns(this.searchForTextBox.Text,
 this.searchInComboBox.Text, this.cur_vals_idx,
 int.Parse(this.dsplySizeComboBox.Text), this.my_org_id, trnsid,
            this.numericUpDown1.Value, this.numericUpDown2.Value, this.orderByComboBox.Text);
      }
      else
      {
        if (this.limitSearchCheckBox.Checked)
        {
          dtst = Global.get_Transactions(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.cur_vals_idx,
        int.Parse(this.dsplySizeComboBox.Text), this.my_org_id, this.accntid,
            this.numericUpDown1.Value, this.numericUpDown2.Value, this.orderByComboBox.Text);
        }
        else
        {
          if (this.dte1 == "" || this.dte2 == "")
          {
            dtst = Global.get_Transactions(this.searchForTextBox.Text,
          this.searchInComboBox.Text, this.cur_vals_idx,
          int.Parse(this.dsplySizeComboBox.Text), this.my_org_id,
            this.numericUpDown1.Value, this.numericUpDown2.Value, this.orderByComboBox.Text);
          }
          else
          {
            dtst = Global.get_Transactions(this.searchForTextBox.Text,
  this.searchInComboBox.Text, this.cur_vals_idx,
  int.Parse(this.dsplySizeComboBox.Text), this.my_org_id,
  this.dte1, this.dte2, true,
            this.numericUpDown1.Value, this.numericUpDown2.Value, this.orderByComboBox.Text);
            ofst = 3;
          }
        }
      }
      this.trnsDetListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_vals_num = this.myNav.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (this.myNav.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][4].ToString()).ToString("#,##0.00"),
    double.Parse(dtst.Tables[0].Rows[i][5].ToString()).ToString("#,##0.00"),
    Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][7].ToString())),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][8].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][10].ToString()).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][9].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][11+ofst].ToString()).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][12+ofst].ToString(),
    dtst.Tables[0].Rows[i][13+ofst].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][14+ofst].ToString()).ToString("#,##0.00"),
    dtst.Tables[0].Rows[i][15+ofst].ToString(),
    dtst.Tables[0].Rows[i][16+ofst].ToString(),
    double.Parse(dtst.Tables[0].Rows[i][17+ofst].ToString()).ToString(),
    double.Parse(dtst.Tables[0].Rows[i][18+ofst].ToString()).ToString(),
    dtst.Tables[0].Rows[i][19+ofst].ToString(),
    dtst.Tables[0].Rows[i][20+ofst].ToString()});
        //if (i % 2 == 1)
        //{
        //  nwItem.BackColor = Color.LightGray;
        //}
        this.trnsDetListView.Items.Add(nwItem);
        ttlDbts += double.Parse(dtst.Tables[0].Rows[i][4].ToString());
        ttlCredits += double.Parse(dtst.Tables[0].Rows[i][5].ToString());

      }
      this.correctValsNavLbls(dtst);

      //this.trnsDetListView.
      ListViewItem nwItem1 = new ListViewItem(new string[] {
    "","","","CURRENT DISPLAY'S TOTALS = ",ttlDbts.ToString("#,##0.00"),
    ttlCredits.ToString("#,##0.00"),
    "","","","","","","","","","","","","","","","",""});
      nwItem1.UseItemStyleForSubItems = false;
      nwItem1.SubItems[3].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
      nwItem1.SubItems[4].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
      nwItem1.SubItems[5].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);

      nwItem1.SubItems[3].BackColor = Color.LightGray;
      nwItem1.SubItems[4].BackColor = Color.LightGray;
      nwItem1.SubItems[5].BackColor = Color.LightGray;

      this.trnsDetListView.Items.Add(nwItem1);

      if (ttlDbts > ttlCredits)
      {
        ttlDbts = ttlDbts - ttlCredits;
        ttlCredits = 0;
      }
      else
      {
        ttlCredits = ttlCredits - ttlDbts;
        ttlDbts = 0;
      }

      if (ttlDbts != 0)
      {
        nwItem1 = new ListViewItem(new string[] {
    "","","","DIFFERENCE = ",ttlDbts.ToString("#,##0.00"),
    "",
    "","","","","","","","","","","","","","","","",""});
      }
      else if (ttlCredits != 0)
      {
        nwItem1 = new ListViewItem(new string[] {
    "","","","DIFFERENCE = ","",
    ttlCredits.ToString("#,##0.00"),
    "","","","","","","","","","","","","","","","",""});
      }

      if (ttlCredits != ttlDbts)
      {
        nwItem1.UseItemStyleForSubItems = false;
        nwItem1.SubItems[3].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
        nwItem1.SubItems[4].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);
        nwItem1.SubItems[5].Font = new Font("Tahoma", 10, FontStyle.Bold | FontStyle.Underline);

        nwItem1.SubItems[3].BackColor = Color.LightGray;
        nwItem1.SubItems[4].BackColor = Color.LightGray;
        nwItem1.SubItems[5].BackColor = Color.LightGray;

        this.trnsDetListView.Items.Add(nwItem1);
      }
      this.obeyEvnts = true;
    }

    /*
     long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.chrt_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_chrt = true;
        this.totl_chrt = 0;
        this.last_chrt_num = 0;
        this.chrt_cur_indx = 0;
        this.updtChrtTotals();
        this.updtChrtNavLabels();
      }
      else if (this.totl_chrt == Global.mnFrm.cmCde.Big_Val
    && totlRecs < int.Parse(this.dsplySizeChrtComboBox.Text))
      {
        this.totl_chrt = this.last_chrt_num;
        if (totlRecs == 0)
        {
          this.chrt_cur_indx -= 1;
          this.updtChrtTotals();
          this.populateChrt();
        }
        else
        {
          this.updtChrtTotals();
        }
      }
     
     */
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
        if (this.searchInComboBox.Text == "Transaction ID")
        {
          long trnsid = -1;
          long.TryParse(this.searchForTextBox.Text.Replace("%", ""), out trnsid);
          this.totl_vals = Global.get_Total_IntfcTrns(this.searchForTextBox.Text,
   this.searchInComboBox.Text, this.my_org_id, trnsid,
            this.numericUpDown1.Value, this.numericUpDown2.Value);
        }
        else
        {
          if (this.limitSearchCheckBox.Checked)
          {
            this.totl_vals = Global.get_Total_Transactions(
              this.searchForTextBox.Text, this.searchInComboBox.Text, this.my_org_id, this.accntid,
            this.numericUpDown1.Value, this.numericUpDown2.Value);
          }
          else
          {
            if (this.dte1 == "" || this.dte2 == "")
            {
              this.totl_vals = Global.get_Total_Transactions(
          this.searchForTextBox.Text, this.searchInComboBox.Text, this.my_org_id,
            this.numericUpDown1.Value, this.numericUpDown2.Value);
            }
            else
            {
              this.totl_vals = Global.get_Total_Transactions(
          this.searchForTextBox.Text, this.searchInComboBox.Text,
          this.my_org_id, this.dte1, this.dte2, true,
            this.numericUpDown1.Value, this.numericUpDown2.Value);
            }
          }
        }
        this.is_last_val = true;
        this.updtValTotals();
        this.cur_vals_idx = this.myNav.totalGroups - 1;
      }
      this.getValPnlData();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
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
      Global.mnFrm.cmCde.exprtToExcelSelective(this.trnsDetListView,
        "SEARCHED TRANSACTIONS FROM " + this.dte1 + " TO " + this.dte2);
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
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.chrtDet_SQL, 10);
    }

    private void rfrshTsrchMenuItem_Click(object sender, EventArgs e)
    {
      this.gotoButton_Click(this.gotoButton, e);
    }

    private void rcHstryTsrchMenuItem_Click(object sender, EventArgs e)
    {
      if (this.trnsDetListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Transaction First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.trnsDetListView.SelectedItems[0].SubItems[8].Text),
        "accb.accb_trnsctn_details", "transctn_id"), 9);
    }

    private void vwSQLTsrchMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLButton_Click(this.vwSQLButton, e);
    }

    private void trnsDetListView_KeyDown(object sender, KeyEventArgs e)
    {
      Global.mnFrm.cmCde.listViewKeyDown(this.trnsDetListView, e);
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      if (this.trnsctnID > 0)
      {
        this.searchInComboBox.SelectedIndex = 4;
      }
      else if (accnt_num == "")
      {
        this.searchInComboBox.SelectedIndex = 1;
      }
      else
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      this.searchForTextBox.Text = "%";
      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.cur_vals_idx = 0;
      this.gotoButton_Click(this.gotoButton, e);
    }

    private void trnsDetListView_DoubleClick(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[2]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.trnsDetListView.SelectedItems.Count <= 0)
      {
        return;
      }
      if (this.trnsDetListView.SelectedItems[0].SubItems[8].Text == "")
      {
        return;
      }
      long trnsID = long.Parse(this.trnsDetListView.SelectedItems[0].SubItems[8].Text);
      string srcTrns = Global.mnFrm.cmCde.getGnrlRecNm("accb.accb_trnsctn_details", "transctn_id",
        "source_trns_ids", trnsID);
      DialogResult dgres;
      if (srcTrns != "," && trnsID != this.trnsctnID)
      {
        vwTrnsctnsDiag nwDiag = new vwTrnsctnsDiag();
        nwDiag.my_org_id = this.my_org_id;
        nwDiag.accnt_name = "";
        nwDiag.accntid = -1;
        nwDiag.trnsctnID = trnsID;
        dgres = nwDiag.ShowDialog();
      }
      else
      {
        trnsAmntBreakDwnDiag nwDiag = new trnsAmntBreakDwnDiag();
        nwDiag.editMode = false;
        nwDiag.trnsaction_id = trnsID;
        dgres = nwDiag.ShowDialog();
      }
      if (dgres == DialogResult.OK)
      {

      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void vwTrnsctnsDiag_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {

        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {

        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {

        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
      {
        if (this.gotoButton.Enabled == true)
        {
          this.gotoButton_Click(this.gotoButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetButton.PerformClick();
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {

        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
        Global.mnFrm.cmCde.listViewKeyDown(this.trnsDetListView, e);
      }
    }

    private void dte1Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
      this.loadValPanel();
    }

    private void dte2Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
      this.loadValPanel();
    }

    private void vldStrtDteTextBox_TextChanged(object sender, EventArgs e)
    {
      this.txtChngd = true;
    }

    private void vldStrtDteTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;

      if (mytxt.Name == "vldStrtDteTextBox")
      {
        this.trnsDte1LOVSrch();
        this.txtChngd = false;
        //this.loadSrchPanel();
      }
      else if (mytxt.Name == "vldEndDteTextBox")
      {
        this.trnsDte2LOVSrch();
        this.txtChngd = false;
        //this.loadSrchPanel();
      }
    }

    private void trnsDte1LOVSrch()
    {
      DateTime dte1 = DateTime.Now;
      bool sccs = DateTime.TryParse(this.vldStrtDteTextBox.Text, out dte1);
      if (!sccs)
      {
        dte1 = DateTime.Now.AddMonths(-6);
      }
      this.vldStrtDteTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss");
    }

    private void trnsDte2LOVSrch()
    {
      DateTime dte1 = DateTime.Now;
      bool sccs = DateTime.TryParse(this.vldEndDteTextBox.Text, out dte1);
      if (!sccs)
      {
        dte1 = DateTime.Now.AddMonths(6);
      }
      this.vldEndDteTextBox.Text = dte1.ToString("dd-MMM-yyyy HH:mm:ss").Replace("00:00:00", "23:59:59");
    }

    private void vldStrtDteTextBox_Click(object sender, EventArgs e)
    {
      TextBox mytxt = (TextBox)sender;

      if (mytxt.Name == "vldStrtDteTextBox")
      {
        this.vldStrtDteTextBox.SelectAll();
      }
      else if (mytxt.Name == "vldEndDteTextBox")
      {
        this.vldEndDteTextBox.SelectAll();
      }
    }

    private void button1_Click(object sender, EventArgs e)
    {
      this.loadValPanel();
    }

    private void button2_Click(object sender, EventArgs e)
    {
      this.loadValPanel();
    }

    private void openBatchMenuItem_Click(object sender, EventArgs e)
    {
      if (this.trnsDetListView.SelectedItems.Count == 1)
      {
        string btchN = this.trnsDetListView.SelectedItems[0].SubItems[21].Text;
        Global.mnFrm.searchForTrnsTextBox.Text = btchN;
        Global.mnFrm.searchInTrnsComboBox.SelectedItem = "Batch Name";
        Global.mnFrm.loadCorrectPanel("Journal Entries");
        Global.mnFrm.showUnpostedCheckBox.Checked = false;
        if (Global.mnFrm.shwMyBatchesCheckBox.Enabled == true)
        {
          Global.mnFrm.shwMyBatchesCheckBox.Checked = false;
        }
        Global.mnFrm.rfrshTrnsButton.PerformClick();
      }
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

  }
}