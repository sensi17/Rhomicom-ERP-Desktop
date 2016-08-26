using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;
using cadmaFunctions;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using System.Net;
using Newtonsoft.Json;
namespace Accounting.Dialogs
{
  public partial class exchangeRatesDiag : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();

    long rec_det_cur_indx = 0;
    bool is_last_rec_det = false;
    long totl_rec_det = 0;
    long last_rec_det_num = 0;
    public string rec_det_SQL = "";

    bool obey_evnts = false;
    public bool addRec = false;
    public bool editRec = false;
    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;
    public int curid = -1;
    public string curCode = "";

    #endregion

    #region "FORM EVENTS..."
    public exchangeRatesDiag()
    {
      InitializeComponent();
    }

    private void exchangeRatesDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.disableFormButtons();
      this.dateTimePicker1.Value = this.dateTimePicker2.Value.AddMonths(-5);
      this.nwRateDateTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);

      this.curid = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      this.curCode = Global.mnFrm.cmCde.getPssblValNm(this.curid);

      this.loadRatesPanel();
      if (this.editRecsP == true)
      {
        this.prpareForLnsEdit();
      }
    }


    public void disableFormButtons()
    {
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[51]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[51]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[51]);

      this.deleteDetButton.Enabled = this.delRecsP;
      this.addButton.Enabled = this.addRecsP;
      //this.saveButton.Enabled = true;
      this.vwSQLDetButton.Enabled = vwSQL;
      this.rcHstryDetButton.Enabled = rcHstry;
    }

    #endregion

    #region "EXCHANGE RATES..."
    private void loadRatesPanel()
    {
      this.obey_evnts = false;
      int dsply = 0;
      if (this.dsplySizeDetComboBox.Text == ""
       || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      this.rec_det_cur_indx = 0;
      this.is_last_rec_det = false;
      this.last_rec_det_num = 0;
      this.totl_rec_det = Global.mnFrm.cmCde.Big_Val;
      this.getTdetPnlData();
      this.obey_evnts = true;
      this.saveLabel.Visible = false;
    }

    private void getTdetPnlData()
    {
      this.updtTdetTotals();
      this.populateTdetGridVw();
      this.updtTdetNavLabels();
    }

    private void updtTdetTotals()
    {
      int dsply = 0;
      if (this.dsplySizeDetComboBox.Text == ""
        || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.myNav.FindNavigationIndices(
    long.Parse(this.dsplySizeDetComboBox.Text), this.totl_rec_det);
      if (this.rec_det_cur_indx >= this.myNav.totalGroups)
      {
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      if (this.rec_det_cur_indx < 0)
      {
        this.rec_det_cur_indx = 0;
      }
      this.myNav.currentNavigationIndex = this.rec_det_cur_indx;
    }

    private void updtTdetNavLabels()
    {
      this.moveFirstDetButton.Enabled = this.myNav.moveFirstBtnStatus();
      this.movePreviousDetButton.Enabled = this.myNav.movePrevBtnStatus();
      this.moveNextDetButton.Enabled = this.myNav.moveNextBtnStatus();
      this.moveLastDetButton.Enabled = this.myNav.moveLastBtnStatus();
      this.positionDetTextBox.Text = this.myNav.displayedRecordsNumbers();
      if (this.is_last_rec_det == true ||
       this.totl_rec_det != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsDetLabel.Text = this.myNav.totalRecordsLabel();
      }
      else
      {
        this.totalRecsDetLabel.Text = "of Total";
      }
    }

    private void populateTdetGridVw()
    {
      this.obey_evnts = false;
      if (this.editRec == false && this.addRec == false)
      {
        this.ratesDataGridView.Rows.Clear();
        //disableLnsEdit();
      }
      else
      {
        prpareForLnsEdit();
      }

      this.obey_evnts = false;
      this.ratesDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      DataSet dtst = Global.get_Rates(this.searchForTextBox.Text, this.searchInComboBox.Text,
        this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00"),
        this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59"),
        this.rec_det_cur_indx,
       int.Parse(this.dsplySizeDetComboBox.Text));
      this.ratesDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_det_num = this.myNav.startIndex() + i;
        this.ratesDataGridView.RowCount += 1;//.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.ratesDataGridView.RowCount - 1;

        this.ratesDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[4].Value = "...";
        this.ratesDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][6].ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[8].Value = "...";
        this.ratesDataGridView.Rows[rowIdx].Cells[9].Value = dtst.Tables[0].Rows[i][8].ToString();
        this.ratesDataGridView.Rows[rowIdx].Cells[10].Value = dtst.Tables[0].Rows[i][0].ToString();
      }
      this.correctTdetNavLbls(dtst);
      this.obey_evnts = true;
    }

    private void correctTdetNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.rec_det_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_rec_det = true;
        this.totl_rec_det = 0;
        this.last_rec_det_num = 0;
        this.rec_det_cur_indx = 0;
        this.updtTdetTotals();
        this.updtTdetNavLabels();
      }
      else if (this.totl_rec_det == Global.mnFrm.cmCde.Big_Val
    && totlRecs < long.Parse(this.dsplySizeDetComboBox.Text))
      {
        this.totl_rec_det = this.last_rec_det_num;
        if (totlRecs == 0)
        {
          this.rec_det_cur_indx -= 1;
          this.updtTdetTotals();
          this.populateTdetGridVw();
        }
        else
        {
          this.updtTdetTotals();
        }
      }
    }

    private bool shdObeyTdetEvts()
    {
      return this.obey_evnts;
    }

    private void TdetPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsDetLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_rec_det = false;
        this.rec_det_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_rec_det = false;
        this.rec_det_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_rec_det = false;
        this.rec_det_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_rec_det = true;
        this.totl_rec_det = Global.get_Total_Rates(this.searchForTextBox.Text, this.searchInComboBox.Text,
        this.dateTimePicker1.Value.ToString("yyyy-MM-dd 00:00:00"),
        this.dateTimePicker2.Value.ToString("yyyy-MM-dd 23:59:59"));
        this.updtTdetTotals();
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getTdetPnlData();
    }

    private void prpareForLnsEdit()
    {
      this.ratesDataGridView.ReadOnly = false;
      this.ratesDataGridView.Columns[0].ReadOnly = true;
      this.ratesDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[1].ReadOnly = true;
      this.ratesDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.ratesDataGridView.Columns[2].ReadOnly = true;
      this.ratesDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[3].ReadOnly = true;
      this.ratesDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[5].ReadOnly = true;
      this.ratesDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.ratesDataGridView.Columns[6].ReadOnly = true;
      this.ratesDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[7].ReadOnly = true;
      this.ratesDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[9].ReadOnly = false;
      this.ratesDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.ratesDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private void disableLnsEdit()
    {
      this.saveLabel.Visible = false;
      this.ratesDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.ratesDataGridView.ReadOnly = true;
      this.ratesDataGridView.Columns[0].ReadOnly = true;
      this.ratesDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[1].ReadOnly = true;
      this.ratesDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[2].ReadOnly = true;
      this.ratesDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[3].ReadOnly = true;
      this.ratesDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[5].ReadOnly = true;
      this.ratesDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[6].ReadOnly = true;
      this.ratesDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[7].ReadOnly = true;
      this.ratesDataGridView.Columns[7].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.ratesDataGridView.Columns[9].ReadOnly = true;
      this.ratesDataGridView.Columns[9].DefaultCellStyle.BackColor = Color.WhiteSmoke;
    }
    #endregion

    private void rfrshDetButton_Click(object sender, EventArgs e)
    {
      this.loadRatesPanel();
    }

    private void findButton_Click(object sender, EventArgs e)
    {
      this.loadRatesPanel();
    }

    private void vwSQLDetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.rates_SQL, 10);
    }

    private void rcHstryDetButton_Click(object sender, EventArgs e)
    {
      if (this.ratesDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.ratesDataGridView.SelectedRows[0].Cells[10].Value.ToString()),
        "accb.accb_exchange_rates", "rate_id"), 9);
    }

    private void dsplySizeDetComboBox_TextChanged(object sender, EventArgs e)
    {
      this.loadRatesPanel();
    }

    private void positionDetTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.TdetPnlNavButtons(this.movePreviousDetButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.TdetPnlNavButtons(this.moveNextDetButton, ex);
      }
    }

    private void searchForTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.searchForTextBox.Focus();
        this.loadRatesPanel();
      }
    }

    private void rateDateButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.nwRateDateTextBox);
      if (this.nwRateDateTextBox.Text.Length > 11)
      {
        this.nwRateDateTextBox.Text = this.nwRateDateTextBox.Text.Substring(0, 11);
      }
      this.dateTimePicker1.Value = DateTime.Parse(this.nwRateDateTextBox.Text);
      this.loadRatesPanel();
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (this.addRecsP == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.nwRateDateTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please Indicate the New Rate Date First!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Create Rates for the Date (" + this.nwRateDateTextBox.Text + ")?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      int funCurID = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
      string funcCurCode = Global.mnFrm.cmCde.getPssblValNm(funCurID);

      DataSet dtst = Global.get_Currencies(funcCurCode);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        if (Global.doesRateExst(this.nwRateDateTextBox.Text,
          dtst.Tables[0].Rows[i][1].ToString(), funcCurCode) == false)
        {
          Global.createRate(this.nwRateDateTextBox.Text,
            dtst.Tables[0].Rows[i][1].ToString(),
            int.Parse(dtst.Tables[0].Rows[i][0].ToString()),
            funcCurCode, funCurID, 1.0000);
        }
      }
      this.dateTimePicker1.Value = DateTime.Parse(this.nwRateDateTextBox.Text);
      this.dateTimePicker2.Value = this.dateTimePicker1.Value;
      this.loadRatesPanel();
      this.prpareForLnsEdit();
    }

    private void deleteDetButton_Click(object sender, EventArgs e)
    {
      if (this.delRecsP == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.ratesDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record(s) to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item(s)?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      for (int i = 0; i < this.ratesDataGridView.SelectedRows.Count; i++)
      {
        long lnID = -1;
        long.TryParse(this.ratesDataGridView.SelectedRows[i].Cells[10].Value.ToString(), out lnID);
        if (this.ratesDataGridView.SelectedRows[i].Cells[0].Value == null)
        {
          this.ratesDataGridView.SelectedRows[i].Cells[0].Value = string.Empty;
        }
        if (this.ratesDataGridView.SelectedRows[i].Cells[1].Value == null)
        {
          this.ratesDataGridView.SelectedRows[i].Cells[1].Value = string.Empty;
        }
        if (this.ratesDataGridView.SelectedRows[i].Cells[5].Value == null)
        {
          this.ratesDataGridView.SelectedRows[i].Cells[5].Value = string.Empty;
        }
        Global.deleteRate(lnID, this.ratesDataGridView.SelectedRows[i].Cells[0].Value.ToString()
          + "/" + this.ratesDataGridView.SelectedRows[i].Cells[1].Value.ToString() +
          "/" + this.ratesDataGridView.SelectedRows[i].Cells[5].Value.ToString());
      }
      this.rfrshDetButton_Click(this.rfrshDetButton, e);
    }

    private void ratesDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_evnts == false)
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obey_evnts;
      this.obey_evnts = false;

      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
      }
      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[1].Value = string.Empty;
      }
      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[2].Value = "-1";
      }
      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[3].Value = string.Empty;
      }
      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[5].Value = "";
      }
      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[6].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[6].Value = "-1";
      }
      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[7].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[7].Value = "";
      }
      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[9].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[9].Value = "0.00";
      }
      if (this.ratesDataGridView.Rows[e.RowIndex].Cells[10].Value == null)
      {
        this.ratesDataGridView.Rows[e.RowIndex].Cells[10].Value = "-1";
      }
      if (e.ColumnIndex == 9)
      {
        double lnAmnt = 0;
        string orgnlAmnt = this.ratesDataGridView.Rows[e.RowIndex].Cells[9].Value.ToString();
        bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
        if (isno == false)
        {
          char[] w = { '0' };
          lnAmnt = Math.Round(Global.computeMathExprsn(orgnlAmnt), 15);
          this.ratesDataGridView.Rows[e.RowIndex].Cells[9].Value = lnAmnt.ToString("#,##0.000000000000000");
        }
        else
        {
          this.ratesDataGridView.Rows[e.RowIndex].Cells[9].Value = lnAmnt.ToString("#,##0.000000000000000");
        }
        long lnID = -1;
        long.TryParse(this.ratesDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString(), out lnID);

        Global.updtRateValue(lnID, Math.Round(lnAmnt, 15));
      }

      this.obey_evnts = true;
    }

    private void exptRatesTmpButton_Click(object sender, EventArgs e)
    {

    }

    private void imptRatesTmpButton_Click(object sender, EventArgs e)
    {

    }

    private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
    {
      if (this.obey_evnts == true)
      {
        this.dateTimePicker2.Value = this.dateTimePicker1.Value;
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";
      this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.rec_det_cur_indx = 0;
      this.rfrshDetButton_Click(this.rfrshDetButton, e);
    }

    private static T _download_serialized_json_data<T>(string url) where T : new()
    {
      using (var w = new WebClient())
      {
        var json_data = string.Empty;
        // attempt to download JSON data as a string
        try
        {
          json_data = w.DownloadString(url);
        }
        catch (Exception) { }
        // if string with JSON data is not empty, deserialize it to class and return its instance 
        return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<T>(json_data) : new T();
      }
    }

    public void updateRates(string dateStr)
    {
      // = Global.getDB_Date_time().Substring(0, 10);
      string rateDte = DateTime.ParseExact(dateStr, "dd-MMM-yyyy",
System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd");

      var url = "https://openexchangerates.org/api/historical/" + rateDte + ".json?app_id=5dba57b2d47b4a11b4e5a020522de567";
      var currencyRates = _download_serialized_json_data<CurrencyRates>(url);
      string baseCur = currencyRates.Base.Trim();
      //Global.mnFrm.cmCde.showMsg(baseCur,0);
      long rateID = -1;
      double rateVal = 0;
      string funcCurCode = this.curCode;
      DataSet dtst = Global.get_Currencies(funcCurCode);
      double baseToFuncCurRate = 0;
      int fromCurID = -1;
      int toCurID = this.curid;
      //Global.mnFrm.cmCde.getPssblValID(funcCurCode, Global.mnFrm.cmCde.getLovID("Currencies"));
      double.TryParse(currencyRates.Rates[funcCurCode].ToString(), out rateVal);
      baseToFuncCurRate = rateVal;

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        fromCurID = Global.mnFrm.cmCde.getPssblValID(dtst.Tables[0].Rows[i][1].ToString(), Global.mnFrm.cmCde.getLovID("Currencies"));
        rateID = Global.doesRateExst1(dateStr, dtst.Tables[0].Rows[i][1].ToString(), funcCurCode);
        double.TryParse(currencyRates.Rates[dtst.Tables[0].Rows[i][1].ToString()].ToString(), out rateVal);

        if (rateVal > 0)
        {
          rateVal = (baseToFuncCurRate / rateVal);
          if (rateID <= 0)
          {
            Global.createRate(dateStr, dtst.Tables[0].Rows[i][1].ToString(), fromCurID, funcCurCode, toCurID,
              rateVal);
          }
          else
          {
            Global.updtRateValue(rateID, rateVal);
          }
        }
      }

    }

    private void downloadRateButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Download Exchange Rates from the Internet?", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      this.saveLabel.Text = "Downloading Exchange Rates...Please Wait...";
      this.saveLabel.Visible = true;
      System.Windows.Forms.Application.DoEvents();
      this.updateRates(this.nwRateDateTextBox.Text);
      this.loadRatesPanel();
      this.saveLabel.Visible = false;
    }

  }
}
