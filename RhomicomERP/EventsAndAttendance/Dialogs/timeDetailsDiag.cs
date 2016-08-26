using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EventsAndAttendance.Classes;

namespace EventsAndAttendance.Dialogs
{
  public partial class timeDetailsDiag : Form
  {
    public timeDetailsDiag()
    {
      InitializeComponent();
    }

    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();

    long rec_det_cur_indx = 0;
    bool is_last_rec_det = false;
    long totl_rec_det = 0;
    long last_rec_det_num = 0;
    public string rec_det_SQL = "";

    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    bool addRec = false;
    bool editRec = false;
    bool beenToCheckBx = false;
    public long attnRecID = -1;
    #endregion

    private void timeDetailsDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      System.Windows.Forms.Application.DoEvents();
      this.loadRgstrDetLnsPanel();
      //if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == true)
      //{
      //  this.prpareForLnsEdit();
      //}
    }

    private void loadRgstrDetLnsPanel()
    {
      this.obey_evnts = false;
      int dsply = 0;
      if (this.dsplySizeDetComboBox.Text == ""
       || int.TryParse(this.dsplySizeDetComboBox.Text, out dsply) == false)
      {
        this.dsplySizeDetComboBox.Text = "10";
      }
      if (this.searchInDetComboBox.SelectedIndex < 0)
      {
        this.searchInDetComboBox.SelectedIndex = 0;
      }
      if (this.searchForDetTextBox.Text.Contains("%") == false)
      {
        this.searchForDetTextBox.Text = "%" + this.searchForDetTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForDetTextBox.Text == "%%")
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
        this.dsplySizeDetComboBox.Text = "10";
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
      this.rgstrDetDataGridView.Rows.Clear();
      if (this.editRec == false && this.addRec == false)
      {
        disableLnsEdit();
      }

      this.obey_evnts = false;
      this.rgstrDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
      DataSet dtst = Global.get_One_AttnRgstr_Times(this.searchForDetTextBox.Text,
        this.searchInDetComboBox.Text,
        this.rec_det_cur_indx,
       int.Parse(this.dsplySizeDetComboBox.Text),
       this.attnRecID);
      this.rgstrDetDataGridView.Rows.Clear();

      int rwcnt = dtst.Tables[0].Rows.Count;
      System.Windows.Forms.Application.DoEvents();
      for (int i = 0; i < rwcnt; i++)
      {
        this.last_rec_det_num = this.myNav.startIndex() + i;
        this.rgstrDetDataGridView.RowCount += 1;//.Insert(this.rgstrDetDataGridView.RowCount - 1, 1);
        int rowIdx = this.rgstrDetDataGridView.RowCount - 1;

        this.rgstrDetDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();

        this.rgstrDetDataGridView.Rows[rowIdx].Cells[0].Value = bool.Parse(dtst.Tables[0].Rows[i][3].ToString());

        this.rgstrDetDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][1].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[2].Value = "...";
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][2].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[4].Value = "...";
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[5].Value = dtst.Tables[0].Rows[i][4].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[6].Value = dtst.Tables[0].Rows[i][0].ToString();
        this.rgstrDetDataGridView.Rows[rowIdx].Cells[7].Value = dtst.Tables[0].Rows[i][7].ToString();

        this.rgstrDetDataGridView.Rows[rowIdx].Cells[8].Value = dtst.Tables[0].Rows[i][6].ToString();
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
        this.totl_rec_det = Global.get_Total_AttnRgstr_Times(this.searchForDetTextBox.Text,
        this.searchInDetComboBox.Text, this.attnRecID);
        this.updtTdetTotals();
        this.rec_det_cur_indx = this.myNav.totalGroups - 1;
      }
      this.getTdetPnlData();
    }

    private void prpareForLnsEdit()
    {

      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.editRec = true;
      this.rgstrDetDataGridView.ReadOnly = false;
      this.rgstrDetDataGridView.Columns[0].ReadOnly = false;
      this.rgstrDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
      this.rgstrDetDataGridView.Columns[1].ReadOnly = false;
      this.rgstrDetDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.White;
      this.rgstrDetDataGridView.Columns[3].ReadOnly = false;
      this.rgstrDetDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.White;

      this.rgstrDetDataGridView.Columns[5].ReadOnly = false;
      this.rgstrDetDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.White;
      this.rgstrDetDataGridView.Columns[8].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.rgstrDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;
    }
    private void disableLnsEdit()
    {

      this.editRec = false;
      this.addRec = false;
      this.rgstrDetDataGridView.DefaultCellStyle.ForeColor = Color.Black;

      this.rgstrDetDataGridView.ReadOnly = true;
      this.rgstrDetDataGridView.Columns[0].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[1].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[3].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.WhiteSmoke;

      this.rgstrDetDataGridView.Columns[5].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[5].DefaultCellStyle.BackColor = Color.WhiteSmoke;
      this.rgstrDetDataGridView.Columns[8].ReadOnly = true;
      this.rgstrDetDataGridView.Columns[8].DefaultCellStyle.BackColor = Color.WhiteSmoke;

    }

    private void rgstrDetDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
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


      this.dfltFill(e.RowIndex);
      if (e.ColumnIndex == 0
        || e.ColumnIndex == 2
        || e.ColumnIndex == 4)
      {

        if (this.addRec == false && this.editRec == false)
        {
          this.prpareForLnsEdit();
        }
        if (this.addRec == false && this.editRec == false)
        {
          Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
          this.obey_evnts = true;
          return;
        }
      }
      if (e.ColumnIndex == 2)
      {
        this.textBox1.Text = this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.textBox1);
        this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[1].Value = this.textBox1.Text;
        this.rgstrDetDataGridView.EndEdit();

        this.obey_evnts = true;
        DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(1, e.RowIndex);
        this.rgstrDetDataGridView_CellValueChanged(this.rgstrDetDataGridView, ex);
      }
      else if (e.ColumnIndex == 4)
      {
        this.textBox2.Text = this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        Global.mnFrm.cmCde.selectDate(ref this.textBox2);
        this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[3].Value = this.textBox2.Text;
        this.rgstrDetDataGridView.EndEdit();

        this.obey_evnts = true;
        DataGridViewCellEventArgs ex = new DataGridViewCellEventArgs(3, e.RowIndex);
        this.rgstrDetDataGridView_CellValueChanged(this.rgstrDetDataGridView, ex);
      }
      this.obey_evnts = true;
    }

    private void dfltFill(int rwIdx)
    {
      if (this.rgstrDetDataGridView.Rows[rwIdx].Cells[0].Value == null)
      {
        this.rgstrDetDataGridView.Rows[rwIdx].Cells[0].Value = false;
      }
      if (this.rgstrDetDataGridView.Rows[rwIdx].Cells[1].Value == null)
      {
        this.rgstrDetDataGridView.Rows[rwIdx].Cells[1].Value = "";
      }

      if (this.rgstrDetDataGridView.Rows[rwIdx].Cells[3].Value == null)
      {
        this.rgstrDetDataGridView.Rows[rwIdx].Cells[3].Value = "";
      }
      if (this.rgstrDetDataGridView.Rows[rwIdx].Cells[5].Value == null)
      {
        this.rgstrDetDataGridView.Rows[rwIdx].Cells[5].Value = "";
      }
      if (this.rgstrDetDataGridView.Rows[rwIdx].Cells[6].Value == null)
      {
        this.rgstrDetDataGridView.Rows[rwIdx].Cells[6].Value = "-1";
      }
      if (this.rgstrDetDataGridView.Rows[rwIdx].Cells[7].Value == null)
      {
        this.rgstrDetDataGridView.Rows[rwIdx].Cells[7].Value = "-1";
      }
      if (this.rgstrDetDataGridView.Rows[rwIdx].Cells[8].Value == null)
      {
        this.rgstrDetDataGridView.Rows[rwIdx].Cells[8].Value = "NO";
      }
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

      this.dfltFill(e.RowIndex);

      if (e.ColumnIndex >= 0 && e.ColumnIndex <= 11)
      {
        this.rgstrDetDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();

        string dtetmin = this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
        string dtetmout = this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
        if (e.ColumnIndex == 1 && dtetmin != "")
        {
          dtetmin = Global.mnFrm.cmCde.checkNFormatDate(dtetmin);
          this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[1].Value = dtetmin;
          this.rgstrDetDataGridView.EndEdit();
          System.Windows.Forms.Application.DoEvents();
        }
        if (e.ColumnIndex == 3 && dtetmout != "")
        {
          dtetmout = Global.mnFrm.cmCde.checkNFormatDate(dtetmout);
          this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[3].Value = dtetmout;
          this.rgstrDetDataGridView.EndEdit();
          System.Windows.Forms.Application.DoEvents();
        }
        long row_id = long.Parse(this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[7].Value.ToString());
        bool isprsnt = (bool)(this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[0].Value);
        string attncmnts = this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[5].Value.ToString();
        string isMain = this.rgstrDetDataGridView.Rows[e.RowIndex].Cells[8].Value.ToString();
        if (isMain == "YES")
        {
          Global.updtAttnRgstrDetLn1(this.attnRecID, dtetmin, dtetmout,
            isprsnt, attncmnts);
        }
        else
        {
          Global.updtAttnRgstrTimeLn(row_id, dtetmin, dtetmout,
            isprsnt, attncmnts);
        }
        this.rgstrDetDataGridView.EndEdit();
      }
      this.obey_evnts = true;
    }

    private void attndRecsForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.Control && e.KeyCode == Keys.S)       // Ctrl-S Save
      {
        this.rgstrDetDataGridView.EndEdit();
        System.Windows.Forms.Application.DoEvents();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.N)       // Ctrl-S Save
      {
        // do what you want here
        this.addVisitorButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.E)       // Ctrl-S Save
      {
        // do what you want here
        this.prpareForLnsEdit();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.rfrshDetButton.PerformClick();
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)      // Ctrl-S Save
      {
        this.rfrshDetButton.PerformClick();
        e.SuppressKeyPress = true;  // stops bing! also sets handeled which stop event bubbling
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {
        this.deleteDetButton.PerformClick();
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;  // stops bing! also sets handeled which stop event bubbling
      }
    }

    private void addVisitorButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[24]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.attnRecID <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a saved Attendance Record First!", 0);
        return;
      }

      if (this.editRec == false)
      {
        this.prpareForLnsEdit();
      }

      Global.createAttnRgstrTimeLn(this.attnRecID, "", "", false, "");
      //this.prpareForLnsEdit();
      this.searchForDetTextBox.Text = "%";
      this.rfrshDetButton.PerformClick();
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.minimizeMemory();

      this.searchInDetComboBox.SelectedIndex = 0;
      this.searchForDetTextBox.Text = "%";
      this.dsplySizeDetComboBox.Text = "10";
      this.disableLnsEdit();
      this.rec_det_cur_indx = 0;
      this.loadRgstrDetLnsPanel();
    }

    private void rcHstryDetButton_Click(object sender, EventArgs e)
    {
      if (this.rgstrDetDataGridView.CurrentCell != null && this.rgstrDetDataGridView.SelectedRows.Count <= 0)
      {
        this.rgstrDetDataGridView.Rows[this.rgstrDetDataGridView.CurrentCell.RowIndex].Selected = true;
      }

      if (this.rgstrDetDataGridView.SelectedRows.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.rgstrDetDataGridView.SelectedRows[0].Cells[10].Value.ToString()),
        "attn.attn_attendance_recs_times", "attnd_det_rec_id"), 7);
    }

    private void vwSQLDetButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_det_SQL, 6);
    }

    private void rfrshDetButton_Click(object sender, EventArgs e)
    {
      this.loadRgstrDetLnsPanel();
    }

    private void deleteDetButton_Click(object sender, EventArgs e)
    {

      if (this.addRec == false && this.editRec == false)
      {
        this.prpareForLnsEdit();
      }
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      if (this.rgstrDetDataGridView.CurrentCell != null && this.rgstrDetDataGridView.SelectedRows.Count <= 0)
      {
        this.rgstrDetDataGridView.Rows[this.rgstrDetDataGridView.CurrentCell.RowIndex].Selected = true;
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
        long.TryParse(this.rgstrDetDataGridView.SelectedRows[i].Cells[7].Value.ToString(), out lnID);
        string isMain = this.rgstrDetDataGridView.SelectedRows[i].Cells[8].Value.ToString();
        if (isMain == "NO")
        {
          Global.deleteAttnTimeLn(lnID, this.Text);
        }
        else { Global.mnFrm.cmCde.showMsg("Cannot Delete Items that came from the Main Page!", 0); }
      }
      this.rfrshDetButton.PerformClick();
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

    private void searchForDetTextBox_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.rfrshDetButton_Click(this.rfrshDetButton, ex);
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.rgstrDetDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      this.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.rgstrDetDataGridView.EndEdit();
      System.Windows.Forms.Application.DoEvents();
      this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.Close();
    }

  }
}
