using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using InternalPayments.Classes;

namespace InternalPayments.Dialogs
{
    public partial class payeRatesDiag : Form
    {
        public payeRatesDiag()
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
        public bool addRec = false;
        public bool editRec = false;
        bool addRecsP = false;
        bool editRecsP = false;
        bool delRecsP = false;
        bool beenToCheckBx = false;
        public int curid = -1;
        public string curCode = "";

        #endregion
        private void payeRatesDiag_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[27]);
            Global.mnFrm.cmCde.selectDataNoParams("select pay.calc_irs_paye_mnthly_tax(216+108+151+4000)");
            this.loadRatesPanel();
            if (this.editRecsP == true)
            {
                this.prpareForLnsEdit();
            }
        }

        #region "EXCHANGE RATES..."
        private void loadRatesPanel()
        {
            this.obey_evnts = false;
            this.populateTdetGridVw();
            this.obey_evnts = true;
        }

        private void populateTdetGridVw()
        {
            this.obey_evnts = false;
            if (this.editRec == false && this.addRec == false && this.editRecsP == false)
            {
                this.ratesDataGridView.Rows.Clear();
                disableLnsEdit();
            }
            else
            {
                prpareForLnsEdit();
            }

            this.obey_evnts = false;
            this.ratesDataGridView.DefaultCellStyle.ForeColor = Color.Black;
            DataSet dtst = Global.get_PayeRates();
            this.ratesDataGridView.Rows.Clear();

            int rwcnt = dtst.Tables[0].Rows.Count;
            for (int i = 0; i < rwcnt; i++)
            {
                this.ratesDataGridView.RowCount += 1;
                int rowIdx = this.ratesDataGridView.RowCount - 1;

                this.ratesDataGridView.Rows[rowIdx].HeaderCell.Value = (i + 1).ToString();
                this.ratesDataGridView.Rows[rowIdx].Cells[0].Value = dtst.Tables[0].Rows[i][0].ToString();
                this.ratesDataGridView.Rows[rowIdx].Cells[1].Value = dtst.Tables[0].Rows[i][1].ToString();
                this.ratesDataGridView.Rows[rowIdx].Cells[2].Value = dtst.Tables[0].Rows[i][2].ToString();
                this.ratesDataGridView.Rows[rowIdx].Cells[3].Value = dtst.Tables[0].Rows[i][3].ToString();
            }
            this.obey_evnts = true;
        }

        private bool shdObeyTdetEvts()
        {
            return this.obey_evnts;
        }

        private void prpareForLnsEdit()
        {
            this.ratesDataGridView.ReadOnly = false;
            this.ratesDataGridView.Columns[0].ReadOnly = true;
            this.ratesDataGridView.Columns[0].DefaultCellStyle.BackColor = Color.WhiteSmoke;
            this.ratesDataGridView.Columns[1].ReadOnly = false;
            this.ratesDataGridView.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.ratesDataGridView.Columns[2].ReadOnly = false;
            this.ratesDataGridView.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);
            this.ratesDataGridView.Columns[3].ReadOnly = false;
            this.ratesDataGridView.Columns[3].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 128);

            this.ratesDataGridView.DefaultCellStyle.ForeColor = Color.Black;
        }

        private void disableLnsEdit()
        {
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
        }
        #endregion

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
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
                this.ratesDataGridView.Rows[e.RowIndex].Cells[0].Value = "-1";
            }
            if (this.ratesDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
            {
                this.ratesDataGridView.Rows[e.RowIndex].Cells[1].Value = "0";
            }
            if (this.ratesDataGridView.Rows[e.RowIndex].Cells[2].Value == null)
            {
                this.ratesDataGridView.Rows[e.RowIndex].Cells[2].Value = "0";
            }
            if (this.ratesDataGridView.Rows[e.RowIndex].Cells[3].Value == null)
            {
                this.ratesDataGridView.Rows[e.RowIndex].Cells[3].Value = "0";
            }
            if (e.ColumnIndex >= 1 && e.ColumnIndex <= 3)
            {
                int levlNo = 0;
                double rateAmnt = 0;
                double txRate = 0;

                double lnAmnt = 0;
                string orgnlAmnt = this.ratesDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                bool isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    char[] w = { '0' };
                    lnAmnt = Global.computeMathExprsn(orgnlAmnt);
                    this.ratesDataGridView.Rows[e.RowIndex].Cells[1].Value = lnAmnt;
                }
                else
                {
                    this.ratesDataGridView.Rows[e.RowIndex].Cells[1].Value = lnAmnt;
                }
                levlNo = (int)lnAmnt;

                lnAmnt = 0;
                orgnlAmnt = this.ratesDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    char[] w = { '0' };
                    lnAmnt = Global.computeMathExprsn(orgnlAmnt);
                    this.ratesDataGridView.Rows[e.RowIndex].Cells[2].Value = lnAmnt;
                }
                else
                {
                    this.ratesDataGridView.Rows[e.RowIndex].Cells[2].Value = lnAmnt;
                }
                rateAmnt = lnAmnt;

                lnAmnt = 0;
                orgnlAmnt = this.ratesDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                isno = double.TryParse(orgnlAmnt, out lnAmnt);
                if (isno == false)
                {
                    char[] w = { '0' };
                    lnAmnt = Global.computeMathExprsn(orgnlAmnt);
                    this.ratesDataGridView.Rows[e.RowIndex].Cells[3].Value = lnAmnt;
                }
                else
                {
                    this.ratesDataGridView.Rows[e.RowIndex].Cells[3].Value = lnAmnt;
                }
                txRate = lnAmnt;

                long lnID = -1;
                long.TryParse(this.ratesDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString(), out lnID);

                Global.updatePayeRates(lnID, levlNo, rateAmnt, txRate, Global.mnFrm.cmCde.getDB_Date_time());
            }

            this.obey_evnts = true;
        }

        private void testPayeButton_Click(object sender, EventArgs e)
        {
            DataSet dt = Global.mnFrm.cmCde.selectDataNoParams("select pay.calc_irs_paye_mnthly_tax(" + this.numericUpDown1.Value + ")");
            if (dt.Tables[0].Rows.Count > 0)
            {
                Global.mnFrm.cmCde.showMsg(dt.Tables[0].Rows[0][0].ToString(), 3);
            }
        }

        private void rfrshDetButton_Click(object sender, EventArgs e)
        {
            this.loadRatesPanel();
        }

        private void rcHstryDetButton_Click(object sender, EventArgs e)
        {
            if (this.ratesDataGridView.CurrentCell != null)
            {
                this.ratesDataGridView.Rows[this.ratesDataGridView.CurrentCell.RowIndex].Selected = true;
            }
            if (this.ratesDataGridView.SelectedRows.Count <= 0)
            {
                Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
                return;
            }
            Global.mnFrm.cmCde.showRecHstry(
              Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
              this.ratesDataGridView.SelectedRows[0].Cells[0].Value.ToString()),
              "pay.pay_paye_rates", "rates_id"), 7);
        }

        private void vwSQLDetButton_Click(object sender, EventArgs e)
        {
            Global.mnFrm.cmCde.showSQL(Global.payeRatesSQL, 8);
        }

        private void deleteDetButton_Click(object sender, EventArgs e)
        {
            if (this.editRecsP == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            if (this.ratesDataGridView.CurrentCell != null)
            {
                this.ratesDataGridView.Rows[this.ratesDataGridView.CurrentCell.RowIndex].Selected = true;
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
                long.TryParse(this.ratesDataGridView.SelectedRows[i].Cells[0].Value.ToString(), out lnID);
                Global.mnFrm.cmCde.deleteGnrlRecs(lnID, "PAYE Rate", "pay.pay_paye_rates", "rates_id");
            }
            this.rfrshDetButton_Click(this.rfrshDetButton, e);
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (this.editRecsP == false)
            {
                Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
                  " this action!\nContact your System Administrator!", 0);
                return;
            }
            Global.createPayeRates(0, 0, 0, Global.mnFrm.cmCde.getDB_Date_time());

            this.loadRatesPanel();
            this.prpareForLnsEdit();
        }
    }
}
