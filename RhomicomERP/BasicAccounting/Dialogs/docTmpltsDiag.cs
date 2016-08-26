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
  public partial class docTmpltsDiag : Form
  {
    public docTmpltsDiag()
    {
      InitializeComponent();
    }

    //Chart of Accounts Panel Variables;
    Int64 tmplt_cur_indx = 0;
    bool is_last_tmplt = false;
    Int64 totl_tmplt = 0;
    long last_tmplt_num = 0;
    bool obey_tmplt_evnts = false;
    bool addtmplt = false;
    bool edittmplt = false;
    public long[] prsnIDs = new long[1];
    public int orgID = -1;
    private bool beenToCheckBx = false;
    bool addTmplts = false;
    bool editTmplts = false;
    bool delTmplts = false;

    #region "FORM EVENTS..."
    private void disableFormButtons()
    {
      this.addTmplts = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[52]);
      this.editTmplts = this.addTmplts;
      this.delTmplts = this.addTmplts;

      this.addButton.Enabled = this.addTmplts;

      this.editButton.Enabled = this.editTmplts;
      this.deleteButton.Enabled = this.delTmplts;

      this.rcHstryButton.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
      this.vwSQLButton.Enabled = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
    }

    private void docTmpltsDiag_Load(object sender, EventArgs e)
    {
      this.obey_tmplt_evnts = false;
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.disableFormButtons();

      string[] tmpltNm = { "Customer Prepayment", "Goods Received Payment", 
                          "Payment of Customer Goods Delivered", "Supplier Prepayment",
                     "Refund-Return of Goods Delivered","Refund-Supplier's Goods/Services Returned" };
      string[] tmpltDesc = { "Customer Prepayment", "Goods Received Payment", 
                            "Payment of Customer Goods Delivered", "Supplier Prepayment",
                     "Refund-Return of Goods Delivered","Refund-Supplier's Goods/Services Returned" };
      string[] docType = { "Customer Advance Payment", "Supplier Standard Payment", 
                               "Customer Standard Payment", "Supplier Advance Payment",
                         "Customer Debit Memo (Indirect-Refund)", "Supplier Credit Memo (Indirect-Refund)"};
      string[] lneTypDec = { "Customer Prepayment", "Cost of Goods Received", 
                          "Initial Price of Goods Delivered", "Supplier Prepayment",
                     "Initial Price of Goods/Services Returned","Initial Price of Goods/Services Returned"};
      for (int f = 0; f < tmpltNm.Length; f++)
      {
        long oldTmpltID = Global.mnFrm.cmCde.getGnrlRecID(
         "accb.accb_doc_tmplts_hdr", "doc_tmplt_name", "doc_tmplts_hdr_id",
         tmpltNm[f], Global.mnFrm.cmCde.Org_id);
        if (oldTmpltID <= 0)
        {
          Global.createDocTmpltHdr(Global.mnFrm.cmCde.Org_id, tmpltNm[f],
            tmpltDesc[f], docType[f], true);
          System.Windows.Forms.Application.DoEvents();
          oldTmpltID = Global.mnFrm.cmCde.getGnrlRecID(
         "accb.accb_doc_tmplts_hdr", "doc_tmplt_name", "doc_tmplts_hdr_id",
         tmpltNm[f], Global.mnFrm.cmCde.Org_id);

          string lineTypeNm = "1Initial Amount";
          string incrDcrs = "Increase";
          int accntID = -1;
          int codeBhndID = -1;
          string lineDesc = lneTypDec[f];
          bool autoCalc = false;
          Global.createDocTmpltDet(oldTmpltID,
            lineTypeNm, lineDesc, autoCalc, incrDcrs, accntID, codeBhndID);
        }
      }
      this.loadTmpltPanel();
      this.obey_tmplt_evnts = true;
    }
    #endregion

    #region "ASSIGNMENT TEMPLATES..."
    private void loadTmpltPanel()
    {
      this.obey_tmplt_evnts = false;
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
      this.is_last_tmplt = false;
      this.totl_tmplt = Global.mnFrm.cmCde.Big_Val;
      this.getTmpltPnlData();
      this.obey_tmplt_evnts = true;
    }

    private void getTmpltPnlData()
    {
      this.updtTmpltTotals();
      this.populateTmplt();
      this.updtTmpltNavLabels();
    }

    private void updtTmpltTotals()
    {
      Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
        int.Parse(this.dsplySizeComboBox.Text), this.totl_tmplt);
      if (this.tmplt_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
      {
        this.tmplt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      if (this.tmplt_cur_indx < 0)
      {
        this.tmplt_cur_indx = 0;
      }
      Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.tmplt_cur_indx;
    }

    private void updtTmpltNavLabels()
    {
      this.moveFirstButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_tmplt == true ||
        this.totl_tmplt != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecsLabel.Text = "of Total";
      }
    }

    private void populateTmplt()
    {
      this.obey_tmplt_evnts = false;
      this.clearTmpltInfo();
      this.disableTmpltEdit();
      this.obey_tmplt_evnts = false;
      this.tmpltsListView.Items.Clear();
      DataSet dtst = Global.get_DocTmpltsHdr(
        this.searchForTextBox.Text,
        this.searchInComboBox.Text,
        this.tmplt_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text),
        Global.mnFrm.cmCde.Org_id);

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_tmplt_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(),dtst.Tables[0].Rows[i][3].ToString()
        ,dtst.Tables[0].Rows[i][4].ToString()});
        this.tmpltsListView.Items.Add(nwItem);
      }
      if (this.tmpltsListView.Items.Count > 0)
      {
        this.obey_tmplt_evnts = true;
        this.tmpltsListView.Items[0].Selected = true;
      }
      else
      {
        this.populateTmpltDet(-1000010);
      }
      this.correctNavLbls(dtst);
      this.obey_tmplt_evnts = true;
    }

    private void populateTmpltDet(int tmpltID)
    {
      this.obey_tmplt_evnts = false;
      this.clearTmpltInfo();
      this.disableTmpltEdit();
      this.obey_tmplt_evnts = false;
      DataSet dtst = Global.get_DocTmpltsDet(tmpltID);
      this.obey_tmplt_evnts = false;
      if (this.tmpltsListView.SelectedItems.Count > 0)
      {
        this.tmpltIDTextBox.Text = this.tmpltsListView.SelectedItems[0].SubItems[2].Text;
        this.tmpltNameTextBox.Text = this.tmpltsListView.SelectedItems[0].SubItems[1].Text;
        this.tmpltDescTextBox.Text = this.tmpltsListView.SelectedItems[0].SubItems[3].Text;
        this.docTypeComboBox.SelectedItem = this.tmpltsListView.SelectedItems[0].SubItems[4].Text;
        this.isEnbldCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          this.tmpltsListView.SelectedItems[0].SubItems[5].Text);
      }
      this.tmpltsDataGridView.DefaultCellStyle.ForeColor = Color.Black;

      this.tmpltsDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < rwcnt; i++)
      {
        this.tmpltsDataGridView.RowCount += 1;//.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.tmpltsDataGridView.RowCount - 1;

        this.tmpltsDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
        this.tmpltsDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.tmpltsDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.tmpltsDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.tmpltsDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[4].Value = dtst.Tables[0].Rows[i][3].ToString();

        int chrgAcntID = int.Parse(dtst.Tables[0].Rows[i][4].ToString());
        this.tmpltsDataGridView.Rows[rowIdx].Cells[6].Value = chrgAcntID.ToString();
        this.tmpltsDataGridView.Rows[rowIdx].Cells[5].Value = Global.mnFrm.cmCde.getAccntNum(chrgAcntID) + "." +
          Global.mnFrm.cmCde.getAccntName(chrgAcntID);

        this.tmpltsDataGridView.Rows[rowIdx].Cells[7].Value = "...";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[8].Value = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][5].ToString());
        this.tmpltsDataGridView.Rows[rowIdx].Cells[9].Value = "Delete";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[10].Value = dtst.Tables[0].Rows[i][6].ToString();
      }
      this.obey_tmplt_evnts = true;
    }

    private void correctNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.tmplt_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_tmplt = true;
        this.totl_tmplt = 0;
        this.last_tmplt_num = 0;
        this.tmplt_cur_indx = 0;
        this.updtTmpltTotals();
        this.updtTmpltNavLabels();
      }
      else if (this.totl_tmplt == Global.mnFrm.cmCde.Big_Val
  && totlRecs < int.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_tmplt = this.last_tmplt_num;
        if (totlRecs == 0)
        {
          this.tmplt_cur_indx -= 1;
          this.updtTmpltTotals();
          this.populateTmplt();
        }
        else
        {
          this.updtTmpltTotals();
        }
      }
    }

    private void clearTmpltInfo()
    {
      this.obey_tmplt_evnts = false;
      this.saveButton.Enabled = false;
      this.addButton.Enabled = this.addTmplts;
      this.editButton.Enabled = this.editTmplts;
      this.deleteButton.Enabled = this.delTmplts;
      if (this.addtmplt == false && this.edittmplt == false)
      {
        this.tmpltIDTextBox.Text = "-1";
        this.tmpltNameTextBox.Text = "";
        this.tmpltDescTextBox.Text = "";
        this.isEnbldCheckBox.Checked = false;
        this.docTypeComboBox.SelectedIndex = -1;
        //this.tmpltsListView.Items.Clear();
        this.tmpltsDataGridView.Rows.Clear();
      }
      this.obey_tmplt_evnts = true;
    }

    private void prpareForTmpltEdit()
    {
      this.saveButton.Enabled = true;
      this.tmpltNameTextBox.ReadOnly = false;
      this.tmpltNameTextBox.BackColor = Color.FromArgb(255, 255, 118);
      this.tmpltDescTextBox.ReadOnly = false;
      this.tmpltDescTextBox.BackColor = Color.White;

      this.docTypeComboBox.BackColor = Color.FromArgb(255, 255, 118);

      this.tmpltsDataGridView.ReadOnly = false;
      this.tmpltsDataGridView.Columns[1].ReadOnly = false;
      this.tmpltsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.tmpltsDataGridView.Columns[2].ReadOnly = false;
      this.tmpltsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.White;
      this.tmpltsDataGridView.Columns[4].ReadOnly = false;
      this.tmpltsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.tmpltsDataGridView.Columns[5].ReadOnly = false;
      this.tmpltsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.tmpltsDataGridView.Columns[8].ReadOnly = false;
      this.tmpltsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.White;

      this.tmpltsDataGridView.DefaultCellStyle.ForeColor = Color.Black;

    }

    private void disableTmpltEdit()
    {
      this.addtmplt = false;
      this.edittmplt = false;
      this.tmpltNameTextBox.ReadOnly = true;
      this.tmpltNameTextBox.BackColor = Color.WhiteSmoke;
      this.tmpltDescTextBox.ReadOnly = true;
      this.tmpltDescTextBox.BackColor = Color.WhiteSmoke;

      this.docTypeComboBox.BackColor = Color.FromArgb(255, 255, 118);

      this.tmpltsDataGridView.ReadOnly = true;
      this.tmpltsDataGridView.Columns[1].ReadOnly = true;
      this.tmpltsDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmpltsDataGridView.Columns[2].ReadOnly = true;
      this.tmpltsDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmpltsDataGridView.Columns[4].ReadOnly = true;
      this.tmpltsDataGridView.Columns[4].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmpltsDataGridView.Columns[5].ReadOnly = true;
      this.tmpltsDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.tmpltsDataGridView.Columns[8].ReadOnly = true;
      this.tmpltsDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.tmpltsDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }

    private bool shdObeyTmpltEvts()
    {
      return this.obey_tmplt_evnts;
    }

    private void TmpltPnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_tmplt = false;
        this.tmplt_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_tmplt = false;
        this.tmplt_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_tmplt = false;
        this.tmplt_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_tmplt = true;
        this.totl_tmplt = Global.get_Total_DocTmpltsHdr(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id);
        this.updtTmpltTotals();
        this.tmplt_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getTmpltPnlData();
    }
    #endregion

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      if (Global.pyblsFrm != null)
      {
        Global.mnFrm.cmCde.showSQL(Global.pyblsFrm.docTmplt_SQL, 10);
        return;
      }
      if (Global.rcvblsFrm != null)
      {
        Global.mnFrm.cmCde.showSQL(Global.rcvblsFrm.docTmplt_SQL, 10);
        return;
      }

    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.tmpltsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.tmpltsListView.SelectedItems[0].SubItems[2].Text),
        "accb.accb_doc_tmplts_hdr", "doc_tmplts_hdr_id"), 9);

    }

    private void positionDetTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.TmpltPnlNavButtons(this.movePreviousButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.TmpltPnlNavButtons(this.moveNextButton, ex);
      }
    }

    private void searchForTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.loadTmpltPanel();
      }
    }

    private void rfrshButton_Click(object sender, EventArgs e)
    {
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";
      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.tmplt_cur_indx = 0;
      this.loadTmpltPanel();
    }

    private void isEnbldCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyTmpltEvts() == false
       || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addtmplt == false && this.edittmplt == false)
      {
        this.isEnbldCheckBox.Checked = !this.isEnbldCheckBox.Checked;
      }
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (this.addTmplts == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
          " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearTmpltInfo();
      if (this.edittmplt == false)
      {
        this.addtmplt = true;
        this.edittmplt = false;
      }
      else
      {
        this.addtmplt = false;
        this.edittmplt = true;
      }
      this.addButton.Enabled = true;
      this.editButton.Enabled = false;
      this.deleteButton.Enabled = false;

      this.createTrnsRows(1);
      this.prpareForTmpltEdit();
    }


    public void createTrnsRows(int num)
    {
      this.obey_tmplt_evnts = false;
      int nwIdx = 0;
      for (int i = 0; i < num; i++)
      {
        //this.tmpltsDataGridView.RowCount += 1;
        //int rowIdx = this.tmpltsDataGridView.RowCount - 1;
        int rowIdx = this.tmpltsDataGridView.RowCount;
        if (this.tmpltsDataGridView.CurrentCell != null)
        {
          rowIdx = this.tmpltsDataGridView.CurrentCell.RowIndex + 1;
        }
        this.tmpltsDataGridView.Rows.Insert(rowIdx, 1);
        this.tmpltsDataGridView.Rows[rowIdx].Cells[0].Value = "-1";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[1].Value = "1Initial Amount";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[2].Value = "";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[3].Value = "...";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[4].Value = "Increase";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[5].Value = "";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[6].Value = "-1";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[7].Value = "...";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[8].Value = false;
        this.tmpltsDataGridView.Rows[rowIdx].Cells[9].Value = "Delete";
        this.tmpltsDataGridView.Rows[rowIdx].Cells[10].Value = "-1";
        nwIdx = rowIdx;
      }

      for (int i = 0; i < this.tmpltsDataGridView.Rows.Count; i++)
      {
        this.tmpltsDataGridView.Rows[i].HeaderCell.Value = (i + 1).ToString();
      }
      //this.tmpltsDataGridView.BeginEdit(false);
      this.obey_tmplt_evnts = true;
      this.tmpltsDataGridView.ClearSelection();
      this.tmpltsDataGridView.Focus();
      //System.Windows.Forms.Application.DoEvents();
      this.tmpltsDataGridView.CurrentCell = this.tmpltsDataGridView.Rows[nwIdx].Cells[1];
      //System.Windows.Forms.Application.DoEvents();
      this.tmpltsDataGridView.BeginEdit(true);
      //System.Windows.Forms.Application.DoEvents();
      //SendKeys.Send("{TAB}");
      SendKeys.Send("{HOME}");
      //System.Windows.Forms.Application.DoEvents();
    }

    private void dfltFill(int rwIdx)
    {
      if (this.tmpltsDataGridView.Rows[rwIdx].Cells[0].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwIdx].Cells[0].Value = "-1";
      }
      if (this.tmpltsDataGridView.Rows[rwIdx].Cells[1].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwIdx].Cells[1].Value = string.Empty;
      }
      if (this.tmpltsDataGridView.Rows[rwIdx].Cells[2].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwIdx].Cells[2].Value = string.Empty;
      }
      if (this.tmpltsDataGridView.Rows[rwIdx].Cells[4].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwIdx].Cells[4].Value = string.Empty;
      }
      if (this.tmpltsDataGridView.Rows[rwIdx].Cells[6].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwIdx].Cells[6].Value = "-1";
      }
      if (this.tmpltsDataGridView.Rows[rwIdx].Cells[5].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwIdx].Cells[5].Value = "";
      }
      if (this.tmpltsDataGridView.Rows[rwIdx].Cells[8].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwIdx].Cells[8].Value = false;
      }
      if (this.tmpltsDataGridView.Rows[rwIdx].Cells[10].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwIdx].Cells[10].Value = "-1";
      }
    }

    private void tmpltsDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_tmplt_evnts == false 
        || (this.addtmplt == false && this.edittmplt == false))
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obey_tmplt_evnts;
      this.obey_tmplt_evnts = false;
      this.dfltFill(e.RowIndex);

      if (e.ColumnIndex == 3)
      {
        string lineTypeNm = this.tmpltsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
        string lovNm = "";
        if (lineTypeNm != "2Tax" && lineTypeNm != "3Discount" && lineTypeNm != "4Extra Charge")
        {
          Global.mnFrm.cmCde.showMsg("Line Type (" + lineTypeNm + ") does not need a Code Behind!)", 0);
          this.obey_tmplt_evnts = true;
          return;
        }
        else if (lineTypeNm == "2Tax")
        {
          lovNm = "Tax Codes";
        }
        else if (lineTypeNm == "3Discount")
        {
          lovNm = "Discount Codes";
        }
        else if (lineTypeNm == "4Extra Charge")
        {
          lovNm = "Extra Charges";
        }
        string srchWrd = this.tmpltsDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
        if (!srchWrd.Contains("%"))
        {
          srchWrd = "%" + srchWrd + "%";
          this.tmpltsDataGridView.Rows[e.RowIndex].Cells[10].Value = "-1";
        }

        string[] selVals = new string[1];
        selVals[0] = this.tmpltsDataGridView.Rows[e.RowIndex].Cells[10].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovNm),
          ref selVals, true, false, Global.mnFrm.cmCde.Org_id,
          srchWrd, "Both", false);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.obey_tmplt_evnts = false;
            this.tmpltsDataGridView.Rows[e.RowIndex].Cells[10].Value = selVals[i];

            this.tmpltsDataGridView.Rows[e.RowIndex].Cells[2].Value = Global.mnFrm.cmCde.getGnrlRecNm(
              "scm.scm_tax_codes", "code_id", "code_name",
              long.Parse(selVals[i]));
          }
        }
        //SendKeys.Send("{Tab}"); 
        //SendKeys.Send("{Tab}"); 
        this.tmpltsDataGridView.EndEdit();
        this.obey_tmplt_evnts = true;
        this.tmpltsDataGridView.CurrentCell = this.tmpltsDataGridView.Rows[e.RowIndex].Cells[8];
      }
      else if (e.ColumnIndex == 7)
      {
        string srchWrd = this.tmpltsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
        if (!srchWrd.Contains("%"))
        {
          srchWrd = "%" + srchWrd + "%";
          //this.tmpltsDataGridView.Rows[e.RowIndex].Cells[4].Value = "-1";
        }

        string[] selVals = new string[1];
        selVals[0] = this.tmpltsDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
        DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Transaction Accounts"),
          ref selVals, true, true, Global.mnFrm.cmCde.Org_id,
          srchWrd, "Both", false);
        if (dgRes == DialogResult.OK)
        {
          for (int i = 0; i < selVals.Length; i++)
          {
            this.obey_tmplt_evnts = false;
            this.tmpltsDataGridView.Rows[e.RowIndex].Cells[6].Value = selVals[i];

            this.tmpltsDataGridView.Rows[e.RowIndex].Cells[5].Value = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) +
    "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
          }
        }
        //SendKeys.Send("{Tab}"); 
        //SendKeys.Send("{Tab}"); 
        this.tmpltsDataGridView.EndEdit();
        this.obey_tmplt_evnts = true;
        this.tmpltsDataGridView.CurrentCell = this.tmpltsDataGridView.Rows[e.RowIndex].Cells[8];
      }
      else if (e.ColumnIndex == 9)
      {
        if (this.delTmplts == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.tmpltsDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
        {
          this.tmpltsDataGridView.Rows.RemoveAt(e.RowIndex);
          return;
        }
        if (this.tmpltsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() == "-1")
        {
          this.tmpltsDataGridView.Rows.RemoveAt(e.RowIndex);
          return;
        }
        if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Template?" +
  "\r\nThis action cannot be undone!", 1) == DialogResult.No)
        {
          Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
          return;
        }

        Global.deleteTmpltDet(long.Parse(this.tmpltsDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString()),
          this.tmpltNameTextBox.Text);
        this.loadTmpltPanel();
      }
      this.obey_tmplt_evnts = true;
    }

    private void tmpltsDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obey_tmplt_evnts == false || (this.addtmplt == false && this.edittmplt == false))
      {
        return;
      }

      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }

      bool prv = this.obey_tmplt_evnts;
      this.obey_tmplt_evnts = false;
      this.dfltFill(e.RowIndex);

      if (e.ColumnIndex == 2)
      {
        string lineTypeNm = this.tmpltsDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
        if (lineTypeNm != "2Tax" && lineTypeNm != "3Discount" && lineTypeNm != "4Extra Charge")
        {
          //Global.mnFrm.cmCde.showMsg("Line Type (" + lineTypeNm + ") does not need a Code Behind!)", 0);
          this.obey_tmplt_evnts = true;
          return;
        }
        this.obey_tmplt_evnts = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(3, e.RowIndex);
        this.tmpltsDataGridView.EndEdit();
        this.tmpltsDataGridView_CellContentClick(this.tmpltsDataGridView, e1);
      }
      else if (e.ColumnIndex == 5)
      {
        this.obey_tmplt_evnts = true;
        DataGridViewCellEventArgs e1 = new DataGridViewCellEventArgs(7, e.RowIndex);
        this.tmpltsDataGridView.EndEdit();
        this.tmpltsDataGridView_CellContentClick(this.tmpltsDataGridView, e1);
      }
      this.tmpltsDataGridView.EndEdit();
      this.obey_tmplt_evnts = true;
    }

    private void tmpltsDataGridView_CurrentCellChanged(object sender, EventArgs e)
    {
      if (this.tmpltsDataGridView.CurrentCell == null || this.obey_tmplt_evnts == false || (this.addtmplt == false && this.edittmplt == false))
      {
        return;
      }
      int rwidx = this.tmpltsDataGridView.CurrentCell.RowIndex;
      int colidx = this.tmpltsDataGridView.CurrentCell.ColumnIndex;

      if (rwidx < 0 || colidx < 0)
      {
        return;
      }
      if (this.tmpltsDataGridView.Rows[rwidx].Cells[6].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwidx].Cells[6].Value = "-1";
      }
      if (this.tmpltsDataGridView.Rows[rwidx].Cells[10].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwidx].Cells[10].Value = "-1";
      }
      if (this.tmpltsDataGridView.Rows[rwidx].Cells[1].Value == null)
      {
        this.tmpltsDataGridView.Rows[rwidx].Cells[1].Value = "1Initial Amount";
      }
      bool prv = this.obey_tmplt_evnts;
      this.obey_tmplt_evnts = false;
      this.dfltFill(rwidx);
      if (colidx >= 0)
      {
        int acntID = int.Parse(this.tmpltsDataGridView.Rows[rwidx].Cells[6].Value.ToString());
        this.tmpltsDataGridView.Rows[rwidx].Cells[5].Value = Global.mnFrm.cmCde.getAccntNum(acntID) +
        "." + Global.mnFrm.cmCde.getAccntName(acntID);

        string lineTypeNm = this.tmpltsDataGridView.Rows[rwidx].Cells[1].Value.ToString();
        if (lineTypeNm != "2Tax" && lineTypeNm != "3Discount" && lineTypeNm != "4Extra Charge")
        {
          //Global.mnFrm.cmCde.showMsg("Line Type (" + lineTypeNm + ") does not need a Code Behind!)", 0);
          this.obey_tmplt_evnts = true;
          return;
        }
        else
        {
          int codeID = int.Parse(this.tmpltsDataGridView.Rows[rwidx].Cells[10].Value.ToString());
          if (codeID > 0)
          {
            this.tmpltsDataGridView.Rows[rwidx].Cells[2].Value = Global.mnFrm.cmCde.getGnrlRecNm(
                "scm.scm_tax_codes", "code_id", "code_name",
                codeID);
          }
        }

      }
      this.tmpltsDataGridView.EndEdit();
      this.obey_tmplt_evnts = true;
    }

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editTmplts == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.tmpltsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to Edit!", 0);
        return;
      }
      this.addtmplt = false;
      this.edittmplt = true;
      this.prpareForTmpltEdit();
      this.addButton.Enabled = true;
      this.editButton.Enabled = false;
      this.deleteButton.Enabled = true;
    }

    private void deleteButton_Click(object sender, EventArgs e)
    {
      if (this.delTmplts == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.tmpltsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to DELETE!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Template?" +
"\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }

      Global.deleteTmpltHdrNDet(long.Parse(this.tmpltsListView.SelectedItems[0].SubItems[2].Text),
        this.tmpltsListView.SelectedItems[0].SubItems[1].Text);
      this.loadTmpltPanel();
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addtmplt == true)
      {
        if (this.addTmplts == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if (this.editTmplts == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      if (this.tmpltNameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Template Name!", 0);
        return;
      }
      if (this.docTypeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Document Type!", 0);
        return;
      }
      long oldTmpltID = Global.mnFrm.cmCde.getGnrlRecID(
        "accb.accb_doc_tmplts_hdr", "doc_tmplt_name", "doc_tmplts_hdr_id",
        this.tmpltNameTextBox.Text, Global.mnFrm.cmCde.Org_id);
      if (oldTmpltID > 0
       && this.addtmplt == true)
      {
        Global.mnFrm.cmCde.showMsg("Template Name is already in Use in this Organisation!", 0);
        return;
      }
      if (oldTmpltID > 0
       && this.edittmplt == true
       && oldTmpltID.ToString() != this.tmpltIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Template Name is already in Use in this Organisation!", 0);
        return;
      }

      if (this.addtmplt == true)
      {
        Global.createDocTmpltHdr(Global.mnFrm.cmCde.Org_id, this.tmpltNameTextBox.Text,
          this.tmpltDescTextBox.Text, this.docTypeComboBox.Text, this.isEnbldCheckBox.Checked);
      }
      else if (this.edittmplt == true)
      {
        Global.updtDocTmpltHdr(long.Parse(this.tmpltIDTextBox.Text), this.tmpltNameTextBox.Text,
          this.tmpltDescTextBox.Text, this.docTypeComboBox.Text, this.isEnbldCheckBox.Checked);
      }
      oldTmpltID = Global.mnFrm.cmCde.getGnrlRecID(
        "accb.accb_doc_tmplts_hdr", "doc_tmplt_name", "doc_tmplts_hdr_id",
        this.tmpltNameTextBox.Text, Global.mnFrm.cmCde.Org_id);
      this.tmpltIDTextBox.Text = oldTmpltID.ToString();
      this.addtmplt = false;
      this.edittmplt = true;
      this.saveTmpltGridView();
      this.saveButton.Enabled = true;
    }

    private void saveTmpltGridView()
    {
      //this.saveButton.Enabled = false;
      this.tmpltsDataGridView.EndEdit();
      this.tmpltsListView.Focus();
      System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < this.tmpltsDataGridView.Rows.Count; i++)
      {
        this.dfltFill(i);

        string lineTypeNm = this.tmpltsDataGridView.Rows[i].Cells[1].Value.ToString();
        string incrDcrs = this.tmpltsDataGridView.Rows[i].Cells[4].Value.ToString();
        int accntID = -1;
        int.TryParse(this.tmpltsDataGridView.Rows[i].Cells[6].Value.ToString(), out accntID);
        string isdbtCrdt = Global.mnFrm.cmCde.dbtOrCrdtAccnt(accntID, incrDcrs.Substring(0, 1));
        if (this.docTypeComboBox.Text.Contains("Supplier"))
        {
          if (lineTypeNm == "1Initial Amount" && isdbtCrdt.ToUpper() != "DEBIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a DEBIT Transaction \r\n(i.e. Increase Asset/Expense/Prepaid Expense!)", 0);
            return;
          }
          if (lineTypeNm == "2Tax" && isdbtCrdt.ToUpper() != "DEBIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a DEBIT Transaction \r\n(i.e. Increase Purchase Tax Expense/Decrease Taxes Payable!)", 0);
            return;
          }
          if (lineTypeNm == "3Discount" && isdbtCrdt.ToUpper() != "CREDIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a CREDIT Transaction \r\n(i.e. Increase Purchase Discounts (Contra Expense Account)!)", 0);
            return;
          }
          if (lineTypeNm == "4Extra Charge" && isdbtCrdt.ToUpper() != "DEBIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a DEBIT Transaction \r\n(i.e. Increase Asset/Expense!)", 0);
            return;
          }
          if (lineTypeNm == "5Applied Prepayment" && isdbtCrdt.ToUpper() != "CREDIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a CREDIT Transaction \r\n(i.e. Decrease Prepaid Expense!)", 0);
            return;
          }
        }
        if (this.docTypeComboBox.Text.Contains("Customer"))
        {
          if (lineTypeNm == "1Initial Amount" && isdbtCrdt.ToUpper() != "CREDIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a CREDIT Transaction \r\n(i.e. Increase Revenue/Custmr Advance Payments!)", 0);
            return;
          }
          if (lineTypeNm == "2Tax" && isdbtCrdt.ToUpper() != "CREDIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a CREDIT Transaction \r\n(i.e. Increase Sales Taxes Payable!)", 0);
            return;
          }
          if (lineTypeNm == "3Discount" && isdbtCrdt.ToUpper() != "DEBIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a DEBIT Transaction \r\n(i.e. Increase Sales Discounts!)", 0);
            return;
          }
          if (lineTypeNm == "4Extra Charge" && isdbtCrdt.ToUpper() != "CREDIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a CREDIT Transaction \r\n(i.e. Increase Extra Revenue Account!)", 0);
            return;
          }
          if (lineTypeNm == "5Applied Prepayment" && isdbtCrdt.ToUpper() != "DEBIT")
          {
            Global.mnFrm.cmCde.showMsg("Row " + (i + 1).ToString() + ":- Expecting a DEBIT Transaction \r\n(i.e. Decrease Customer Advance Payments!)", 0);
            return;
          }
        }
        /*Validate the accounting behind these five line Types
              1Initial Amount-DEBIT
              2Tax-DEBIT
              3Discount-CREDIT
              4Extra Charge-DEBIT
              5Applied
              * Prepayment-CREDIT
              */

      }
      int cntr = 0;
      for (int i = 0; i < this.tmpltsDataGridView.Rows.Count; i++)
      {
        string lineTypeNm = this.tmpltsDataGridView.Rows[i].Cells[1].Value.ToString();
        string incrDcrs = this.tmpltsDataGridView.Rows[i].Cells[4].Value.ToString();
        int accntID = -1;
        int.TryParse(this.tmpltsDataGridView.Rows[i].Cells[6].Value.ToString(), out accntID);

        System.Windows.Forms.Application.DoEvents();

        long tmpltDetID = -1;
        long.TryParse(this.tmpltsDataGridView.Rows[i].Cells[0].Value.ToString(), out tmpltDetID);

        int codeBhndID = -1;
        int.TryParse(this.tmpltsDataGridView.Rows[i].Cells[10].Value.ToString(), out codeBhndID);

        string lineDesc = this.tmpltsDataGridView.Rows[i].Cells[2].Value.ToString();
        bool autoCalc = (bool)this.tmpltsDataGridView.Rows[i].Cells[8].Value;
        if (accntID > 0 && lineTypeNm != "" && incrDcrs != "")
        {
          if (tmpltDetID > 0)
          {
            Global.updtDocTmpltDet(tmpltDetID, lineTypeNm, lineDesc, autoCalc, incrDcrs, accntID, codeBhndID);
          }
          else
          {
            Global.createDocTmpltDet(int.Parse(this.tmpltIDTextBox.Text),
              lineTypeNm, lineDesc, autoCalc, incrDcrs, accntID, codeBhndID);
          }
          this.tmpltsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Lime;
          cntr++;
        }
        else
        {
          this.tmpltsDataGridView.Rows[i].DefaultCellStyle.BackColor = Color.Red;
        }
      }
      //Global.mnFrm.cmCde.showMsg(cntr + " Record(s) Saved!", 3);
      //this.saveButton.Enabled = true;
      this.loadTmpltPanel();
    }

    private void tmpltsListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyTmpltEvts() == false)
      {
        return;
      }
      if (this.tmpltsListView.SelectedItems.Count > 0)
      {
        this.populateTmpltDet(int.Parse(this.tmpltsListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateTmpltDet(-1000010);
      }
    }

    private void tmpltsDataGridView_KeyDown(object sender, KeyEventArgs e)
    {
      this.tmpltsDataGridView.EndEdit();
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
        if (this.rfrshButton.Enabled == true)
        {
          this.rfrshButton_Click(this.rfrshButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
      }
    }

    private void tmpltsListView_KeyDown(object sender, KeyEventArgs e)
    {
      this.tmpltsDataGridView.EndEdit();
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
        if (this.rfrshButton.Enabled == true)
        {
          this.rfrshButton_Click(this.rfrshButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        Global.mnFrm.cmCde.listViewKeyDown(this.tmpltsListView, e);
      }
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadTmpltPanel();
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void docTypeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
  }
}
