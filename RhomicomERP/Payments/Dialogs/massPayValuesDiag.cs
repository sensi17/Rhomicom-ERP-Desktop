using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InternalPayments.Classes;
using cadmaFunctions;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;

namespace InternalPayments.Dialogs
{
  public partial class massPayValuesDiag : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();

    public long msPyID = -1;
    public int pyItmSetID = -1;
    public int prsnSetID = -1;
    public string trns_date = "";
    public string glDate = "";
    long rec_det_cur_indx = 0;
    bool is_last_rec_det = false;
    long totl_rec_det = 0;
    long last_rec_det_num = 0;
    public string rec_det_SQL = "";

    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    public bool addRec = false;
    public bool editRec = false;
    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;

    #endregion

    #region "FORM EVENTS..."
    public massPayValuesDiag()
    {
      InitializeComponent();
    }

    private void massPayValuesDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.disableFormButtons();
      this.loadRgstrDetLnsPanel();
      this.vldStrtDteTextBox.Text = this.trns_date;
      this.vldEndDteTextBox.Text = this.trns_date;
      this.timer1.Interval = 500;
      this.timer1.Enabled = true;
    }

    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[7]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[8]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[17]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[18]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[19]);

      this.deleteDetButton.Enabled = this.editRecsP;
      this.vwSQLDetButton.Enabled = vwSQL;
      this.rcHstryDetButton.Enabled = rcHstry;
    }

    #endregion

    #region "ATTENDANCE REGISTERS..."
    private void loadRgstrDetLnsPanel()
    {
      this.obey_evnts = false;
      int dsply = 0;
      if (this.dsplySizeDetComboBox.Text == ""
       || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDetComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      if (this.searchInDetComboBox.SelectedIndex < 0)
      {
        this.searchInDetComboBox.SelectedIndex = 3;
      }
      if (this.searchForDetTextBox.Text == "")
      {
        this.searchForDetTextBox.Text = "%";
      }
      this.rec_det_cur_indx = 0;
      this.is_last_rec_det = false;
      this.last_rec_det_num = 0;
      this.totl_rec_det = Global.mnFrm.cmCde.Big_Val;
      this.getTdetPnlData();
      this.obey_evnts = true;
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
      this.rgstrDetDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      this.obey_evnts = false;
      if (this.editRec == false && this.addRec == false)
      {
        this.rgstrDetDataGridView.Rows.Clear();
        disableLnsEdit();
      }
      else
      {
        prpareForLnsEdit();
      }
      this.shdcancel = false;
      this.cancelRunButton.Enabled = false;
      this.loadRgstrPrsnsButton.Enabled = true;
      this.progressBar1.Value = 0;
      this.progressLabel.Text = "0%";

      this.obey_evnts = false;
      this.rgstrDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      DataSet dtst = Global.get_One_MsPayAtchdVals(this.searchForDetTextBox.Text,
        this.searchInDetComboBox.Text,
        this.rec_det_cur_indx,
       int.Parse(this.dsplySizeDetComboBox.Text),
       this.msPyID);
      this.rgstrDetDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_det_num = this.myNav.startIndex() + i;
        this.rgstrDetDataGridView.RowCount += 1;//.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.rgstrDetDataGridView.RowCount - 1;

        this.rgstrDetDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][3].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][6].ToString();

        this.rgstrDetDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][8].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[5].Value = double.Parse(dtst.Tables[0].Rows[i][9].ToString()).ToString("#,##0.00");
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][5].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][7].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][0].ToString();
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
        this.totl_rec_det = Global.get_Total_MsPayAtchdVals(this.searchForDetTextBox.Text,
        this.searchInDetComboBox.Text, this.msPyID);
        this.updtTdetTotals();
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getTdetPnlData();
    }

    private void prpareForLnsEdit()
    {
      this.rgstrDetDataGridView.ReadOnly = false;
      this.rgstrDetDataGridView.Columns[0].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[2].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[4].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[6].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[3].ReadOnly = false;
      this.rgstrDetDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.rgstrDetDataGridView.Columns[5].ReadOnly = false;
      this.rgstrDetDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

      this.rgstrDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private void disableLnsEdit()
    {
      this.rgstrDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      this.cancelRunButton.Enabled = false;
      this.rgstrDetDataGridView.ReadOnly = true;
      this.rgstrDetDataGridView.Columns[0].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[2].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[4].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[6].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[6].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[3].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.rgstrDetDataGridView.Columns[5].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;

    }
    #endregion

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

    private void searchForDetTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.rfrshDetButton_Click(this.rfrshDetButton, ex);
      }
    }

    private void rfrshDetButton_Click(object sender, EventArgs e)
    {
      this.loadRgstrDetLnsPanel();
    }

    private void rcHstryDetButton_Click(object sender, EventArgs e)
    {
      if (this.rgstrDetDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.rgstrDetDataGridView.SelectedRows[0].Cells[8].Value.ToString()),
        "pay.pay_value_sets_det", "value_set_det_id"), 7);
    }

    private void vwSQLDetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.mspyAtchdVals_SQL1, 8);
    }

    private void deleteDetButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode on the Parent Page First!", 0);
        return;
      }

      if (this.rgstrDetDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record(s) to Delete!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Item?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      for (int i = 0; i < this.rgstrDetDataGridView.SelectedRows.Count; i++)
      {
        long lnID = -1;
        long.TryParse(this.rgstrDetDataGridView.SelectedRows[i].Cells[8].Value.ToString(), out lnID);
        if (this.rgstrDetDataGridView.SelectedRows[i].Cells[2].Value == null)
        {
          this.rgstrDetDataGridView.SelectedRows[i].Cells[2].Value = string.Empty;
        }
        Global.deleteMsPayAtchdVal(lnID, this.rgstrDetDataGridView.SelectedRows[i].Cells[2].Value.ToString());
      }
      this.rfrshDetButton_Click(this.rfrshDetButton, e);
    }

    private void loadRgstrPrsnsButton_Click(object sender, EventArgs e)
    {
      this.cancelRunButton.Enabled = true;
      this.loadRgstrPrsnsButton.Enabled = false;
      this.shdcancel = false;
      this.progressBar1.Value = 0;
      this.progressLabel.Text = "0%";
      if (this.addRec == false && this.editRec == false)
      {
        //Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode on Parent Form First!", 0);
        this.cancelRunButton.Enabled = false;
        this.loadRgstrPrsnsButton.Enabled = false;
        this.shdcancel = false;
        this.progressBar1.Value = 0;
        this.progressLabel.Text = "0%";
        return;
      }

      string dateStr = Global.mnFrm.cmCde.getFrmtdDB_Date_time();
      if (this.msPyID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please indicate the Mass Pay Run First!", 0);
        return;
      }

      //if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Load the Allowed Person(s)\r\n and their Editable Values!", 1) == DialogResult.No)
      //{
      //  //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
      //  return;
      //}
      Global.mnFrm.cmCde.executeGnrlDDLSQL("TRUNCATE pay.pay_balsitm_bals_retro");
      Global.mnFrm.cmCde.executeGnrlDDLSQL("TRUNCATE pay.pay_itm_trnsctns_retro");
      long msg_id = Global.mnFrm.cmCde.getLogMsgID("pay.pay_mass_pay_run_msgs",
        "Mass Pay Run", this.msPyID);
      if (msg_id <= 0)
      {
        Global.mnFrm.cmCde.createLogMsg(dateStr + " .... Mass Pay Run is about to Start...",
    "pay.pay_mass_pay_run_msgs", "Mass Pay Run", this.msPyID, dateStr);
      }

      //Get dataset for Pay Items to pay
      //Get dataset for persons to be paid
      //loop through persons and for each person loop through all pay items
      DataSet itmsDtSt = Global.get_AllEditblItmStDet(this.pyItmSetID);
      DataSet prsDtSt = Global.get_AllPrsStDet(this.prsnSetID);
      int prsCnt = prsDtSt.Tables[0].Rows.Count;
      int itmCnt = itmsDtSt.Tables[0].Rows.Count;

      for (int i = 0; i < prsCnt; i++)
      {
        if (this.shdcancel == true)
        {
          break;
        }
        else
        {
          this.cancelRunButton.Enabled = true;
        }
        System.Windows.Forms.Application.DoEvents();
        this.progressBar1.Value = (int)(((double)(i + 1) / (double)prsCnt) * (double)100);
        this.progressLabel.Text = this.progressBar1.Value.ToString() + "%";
        System.Windows.Forms.Application.DoEvents();
        //Loop through all items to pay them for this person only
        for (int j = 0; j < itmCnt; j++)
        {
          if (this.shdcancel == true)
          {
            break;
          }
          else
          {
            this.cancelRunButton.Enabled = true;
            this.loadRgstrPrsnsButton.Enabled = false;
          }
          //Person Pay Items
          long itmid = long.Parse(itmsDtSt.Tables[0].Rows[j][0].ToString());
          long personid = long.Parse(prsDtSt.Tables[0].Rows[i][0].ToString());
          string itm_maj_typ = itmsDtSt.Tables[0].Rows[j][4].ToString();
          string trns_typ = itmsDtSt.Tables[0].Rows[j][3].ToString();

          if (itm_maj_typ.ToUpper() == "Balance Item".ToUpper())
          {
            continue;
          }
          if (trns_typ == "")
          {
            continue;
          }
          //if (Global.doesPrsnHvItm(personid, itmid, this.trns_date) == false)
          //{
          //  continue;
          //}

          string dteEarned = "";

          string usesSQL = itmsDtSt.Tables[0].Rows[j][6].ToString();
          /*Global.mnFrm.cmCde.getGnrlRecNm(
     "org.org_pay_items", "item_id", "uses_sql_formulas",
     itmid);
          Global.mnFrm.cmCde.getGnrlRecNm(
   "org.org_pay_items", "item_id", "is_retro_element", itmid);*/
          string isRetroElmnt = itmsDtSt.Tables[0].Rows[j][7].ToString();

          // check if item is not balance item
          //check if person has item actively
          //get and make sure trns type is not empty
          //Get trns date
          //Get amount to pay and make sure it is not zero
          //string itm_uom = itmsDtSt.Tables[0].Rows[j][2].ToString();

          double pay_amount = 0;
          long prs_itm_val_id = Global.getPrsnItmVlID(personid, itmid, this.trns_date);
          if (prs_itm_val_id <= 0)
          {
            if (isRetroElmnt == "1")
            {
              prs_itm_val_id = Global.getFirstItmValID(itmid);
            }
            else
            {
              continue;
            }
          }
          if (isRetroElmnt == "1")
          {
            int prntItmID = int.Parse(Global.mnFrm.cmCde.getGnrlRecNm(
  "org.org_pay_items", "retro_item_id", "item_id", itmid));
            string itm_uom = Global.mnFrm.cmCde.getGnrlRecNm(
  "org.org_pay_items", "retro_item_id", "item_value_uom", prntItmID);

            int crncy_id = -1;
            string crncy_cde = itm_uom;
            if (itm_uom == "Money")
            {
              crncy_id = Global.mnFrm.cmCde.getOrgFuncCurID(Global.mnFrm.cmCde.Org_id);
              crncy_cde = Global.mnFrm.cmCde.getPssblValNm(crncy_id);
            }
            DataSet hstryDtSt = Global.get_PstPays(personid, prntItmID,
              this.vldStrtDteTextBox.Text, this.vldEndDteTextBox.Text);
            long valsetdetid = -1;
            for (int p = 0; p < hstryDtSt.Tables[0].Rows.Count; p++)
            {
              dteEarned = hstryDtSt.Tables[0].Rows[p][2].ToString();
              string trnsDteErnd = DateTime.ParseExact(
dteEarned, "yyyy-MM-dd HH:mm:ss",
System.Globalization.CultureInfo.InvariantCulture).ToString("dd-MMM-yyyy HH:mm:ss");
              double oldPayAmnt = double.Parse(hstryDtSt.Tables[0].Rows[p][1].ToString());
              double oldRetroPays = Global.get_PstRetroPaysSum(personid, itmid, trnsDteErnd);

              string retmsg = Global.mnFrm.runRetroMassPay(Global.mnFrm.cmCde.Org_id,
               long.Parse(prsDtSt.Tables[0].Rows[i][0].ToString()),
               prsDtSt.Tables[0].Rows[i][1].ToString(),
               prntItmID,
               int.Parse(itmsDtSt.Tables[0].Rows[j][0].ToString()),
               Global.mnFrm.cmCde.getItmName(prntItmID),
               Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items", "item_id", "item_value_uom", prntItmID),
               this.msPyID, trnsDteErnd, this.trns_date,
               itmsDtSt.Tables[0].Rows[j][3].ToString(),
               Global.mnFrm.cmCde.getItmMajType(prntItmID),
               Global.mnFrm.cmCde.getItmMinType(prntItmID),
               msg_id,
               "pay.pay_mass_pay_run_msgs", dateStr, glDate, ref pay_amount);
              //Global.mnFrm.cmCde.showSQLNoPermsn(personid+"-"+pay_amount + "/" + oldPayAmnt + "/" + oldRetroPays);
              pay_amount = pay_amount - oldPayAmnt - oldRetroPays;

              valsetdetid = Global.doesAtchdValHvPrsn(personid,
this.msPyID, itmid, dteEarned);

              if (valsetdetid <= 0 && pay_amount != 0)
              {
                Global.createMsPayAtchdVal(this.msPyID, personid, itmid,
                  pay_amount, prs_itm_val_id, dteEarned);
              }
            }
          }
          else
          {
            dteEarned = "";
            long valsetdetid = Global.doesAtchdValHvPrsn(personid,
  this.msPyID, itmid, dteEarned);

            string valSQL = "";
            // Global.mnFrm.cmCde.getItmValSQL(prs_itm_val_id);
            if (valSQL == "")
            {
              pay_amount = Global.mnFrm.cmCde.getItmValueAmnt(prs_itm_val_id);
            }
            if (valsetdetid <= 0 && prs_itm_val_id > 0 && valSQL == "" && usesSQL == "0")
            {
              Global.createMsPayAtchdVal(this.msPyID, personid, itmid,
                pay_amount, prs_itm_val_id, dteEarned);
            }
          }

          System.Windows.Forms.Application.DoEvents();
        }
      }
      //Global.mnFrm.cmCde.showMsg("Successfully Loaded the Allowed Persons\r\n and their Editable Values!", 3);
      this.loadRgstrPrsnsButton.Enabled = true;
      this.rfrshDetButton_Click(this.rfrshDetButton, e);
    }

    private void rgstrDetDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_evnts == false || (this.addRec == false && this.editRec == false))
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obey_evnts;
      this.obey_evnts = false;

      if (this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[8].Value == null)
      {
        this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[8].Value = "-1";
      }

      if (this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value == null)
      {
        this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value = "0";
      }
      if (e.ColumnIndex == 5)
      {
        long row_id = long.Parse(this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString());
        double amnt = 0;
        string orgnlAmnt = this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
        bool isnumvld = double.TryParse(this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString().Trim().Replace(",", ""), out amnt);
        if (isnumvld == false)
        {
          this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value = Math.Round(Global.computeMathExprsn(orgnlAmnt), 2);
          //Global.mnFrm.cmCde.showMsg("Value '" + this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString() + "' is an Invalid Number!", 0);
          //this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value = 0;
          this.obey_evnts = true;
          return;
        }
        this.obey_evnts = false;
        Global.updtMsPayAtchdVal(row_id, Math.Round(amnt, 2));
        this.rgstrDetDataGridView.EndEdit();
        this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value = amnt.ToString("#,##0.00");
        System.Windows.Forms.Application.DoEvents();
        this.rgstrDetDataGridView.EndEdit();
      }
      this.obey_evnts = true;
    }

    private void exprtPayValsTmp(int exprtTyp)
    {
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      Global.mnFrm.cmCde.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).ToUpper();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
      Global.mnFrm.cmCde.trgtSheets[0].Shapes.AddPicture(Global.mnFrm.cmCde.getOrgImgsDrctry() + @"\" + Global.mnFrm.cmCde.Org_id + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs = { "Person's ID No.**", "Full Name", "Item Name**", "Item Value Name**", "Value/Amount to Use**" };
      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }
      if (exprtTyp == 2)
      {
        DataSet dtst = Global.get_One_MsPayAtchdVals("%", "Person Name/ID", 0, 100000000, this.msPyID);
        for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
        }
      }
      else if (exprtTyp >= 3)
      {
        DataSet dtst = Global.get_One_MsPayAtchdVals("%", "Person Name/ID", 0, exprtTyp, this.msPyID);
        for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
        {
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][6].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][8].ToString();
          ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
        }
      }
      else
      {
      }

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Columns.AutoFit();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:Z65535", Type.Missing).Rows.AutoFit();
    }

    private void exptPyValuesTmpButton_Click(object sender, EventArgs e)
    {
      if (this.msPyID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Mass Pay First!", 4);
        return;
      }
      string rspnse = Interaction.InputBox("How many Pay Values will you like to Export?" +
        "\r\n1=No Pay Values(Empty Template)" +
        "\r\n2=All Pay Values" +
        "\r\n3-Infinity=Specify the exact number of Pay Values to Export\r\n",
        "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
        (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
      if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int rsponse = 0;
      bool rsps = int.TryParse(rspnse, out rsponse);
      if (rsps == false)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
        return;
      }
      if (rsponse < 1)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting a Number Above Zero", 4);
        return;
      }
      this.exprtPayValsTmp(rsponse);

    }

    private void imprtPyValuesTmp(string filename)
    {
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];
      string prsnLocID = "";
      string itemName = "";
      string itemValNm = "";
      string valAmount = "";
      int rownum = 5;
      do
      {
        try
        {
          prsnLocID = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          prsnLocID = "";
        }
        try
        {
          itemName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itemName = "";
        }
        try
        {
          itemValNm = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          itemValNm = "";
        }
        try
        {
          valAmount = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          valAmount = "";
        }

        if (rownum == 5)
        {
          string[] hdngs = { "Person's ID No.**", "Full Name", "Item Name**", "Item Value Name**", "Value/Amount to Use**" };

          if (prsnLocID != hdngs[0].ToUpper() || itemName != hdngs[2].ToUpper()
            || itemValNm != hdngs[3].ToUpper()
            || valAmount != hdngs[4].ToUpper())
          {
            Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (prsnLocID != "" && itemName != "" && itemValNm != "" && valAmount != "")
        {
          long itmID = Global.mnFrm.cmCde.getItmID(itemName, Global.mnFrm.cmCde.Org_id);
          long prsnID = Global.mnFrm.cmCde.getPrsnID(prsnLocID);
          long valsetdetid = Global.doesAtchdValHvPrsn(prsnID,
            this.msPyID, itmID, "");
          if (valsetdetid <= 0)
          {
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
          }
          else if (valsetdetid > 0)
          {
            double amnt = 0;
            bool isnumvld = double.TryParse(valAmount.Trim().Replace(",", ""), out amnt);

            if (amnt != 0)
            {
              Global.updtMsPayAtchdVal(valsetdetid, amnt);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else
            {
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
            }
          }
          else
          {
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":F" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
            //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
          }
        }
        rownum++;
      }
      while (prsnLocID != "");
    }

    private void imptPyValuesTmpButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode on Parent Form First!", 0);
        return;
      }

      if (this.msPyID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please indicate the Mass Pay Run First!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to Import Values\r\n to Overwrite the existing values shown here?", 1) == DialogResult.No)
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
      this.openFileDialog1.FilterIndex = 2;
      this.openFileDialog1.Title = "Select an Excel File to Upload...";
      this.openFileDialog1.FileName = "";
      if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        this.imprtPyValuesTmp(this.openFileDialog1.FileName);
      }
      this.rfrshDetButton_Click(this.rfrshDetButton, e);

    }
    bool shdcancel = false;
    private void cancelRunButton_Click(object sender, EventArgs e)
    {
      shdcancel = true;
    }

    private void dte1Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldStrtDteTextBox);
    }

    private void dte2Button_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.selectDate(ref this.vldEndDteTextBox);
    }

    private void vldStrtDteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
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
      this.obey_evnts = false;
      this.srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
      }

      if (mytxt.Name == "vldStrtDteTextBox")
      {
        this.vldStrtDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldStrtDteTextBox.Text);
      }
      else if (mytxt.Name == "vldEndDteTextBox")
      {
        this.vldEndDteTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.vldEndDteTextBox.Text);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void vldStrtDteTextBox_Click(object sender, EventArgs e)
    {
      TextBox mytxt = (TextBox)sender;
      mytxt.SelectAll();
    }

    private void timer1_Tick(object sender, EventArgs e)
    {
      this.timer1.Enabled = false;
      if (this.rgstrDetDataGridView.Rows.Count <= 0)
      {
        this.loadRgstrPrsnsButton_Click(this.loadRgstrPrsnsButton, e);
      }
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
  }
}
