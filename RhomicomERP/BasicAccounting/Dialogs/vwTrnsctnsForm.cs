using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BasicAccounting.Classes;

namespace BasicAccounting.Dialogs
	{
	public partial class vwTrnsctnsDiag : Form
		{
		public vwTrnsctnsDiag()
			{
			InitializeComponent();
			}

		private long totl_vals = 0;
		private long cur_vals_idx = 0;
		private string vwSQLStmnt = "";
		private bool is_last_val = false;
		bool obeyEvnts = false;
		long last_vals_num = 0;
		public int my_org_id = 0;
		public string accnt_name = "";
		private void vwTrnsctnsDiag_Load(object sender, EventArgs e)
			{
			this.loadValPanel();
			}

		private void loadValPanel()
			{
			this.obeyEvnts = false;
			if (this.searchInComboBox.SelectedIndex < 0)
				{
				this.searchInComboBox.SelectedIndex = 1;
				}
			if (this.dsplySizeComboBox.Text == "")
				{
				this.dsplySizeComboBox.SelectedIndex = 4;
				}
			if (this.searchForTextBox.Text == "")
				{
				this.searchForTextBox.Text =this.accnt_name;
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
			Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(int.Parse(this.dsplySizeComboBox.Text),
			this.totl_vals);

			if (this.cur_vals_idx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
				{
				this.cur_vals_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
				}
			if (this.cur_vals_idx < 0)
				{
				this.cur_vals_idx = 0;
				}
			Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.cur_vals_idx;
			}

		private void updtValNavLabels()
			{
			this.moveFirstButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
			this.movePreviousButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
			this.moveNextButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
			this.moveLastButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
			this.positionTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
			if (this.is_last_val == true ||
				this.totl_vals != Global.mnFrm.cmCde.Big_Val)
				{
				this.totalRecLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
				}
			else
				{
				this.totalRecLabel.Text = "of Total";
				}
			}

		private void populateValGridVw()
			{
			this.obeyEvnts = false;
			DataSet dtst = Global.get_Transactions(this.searchForTextBox.Text,
				this.searchInComboBox.Text,this.cur_vals_idx,
				int.Parse(this.dsplySizeComboBox.Text),this.my_org_id);
			this.trnsDetListView.Items.Clear();
			if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[1]) == false)
				{
				Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
					" this action!\nContact your System Administrator!", 0);
				this.obeyEvnts = true;
				return;
				}	
			for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
				{
				//;
				ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),dtst.Tables[0].Rows[i][2].ToString(),
    dtst.Tables[0].Rows[i][3].ToString(),
    dtst.Tables[0].Rows[i][4].ToString(),
    dtst.Tables[0].Rows[i][5].ToString(),
    Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[i][7].ToString())),
    dtst.Tables[0].Rows[i][6].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][8].ToString(),
    dtst.Tables[0].Rows[i][7].ToString(),
    dtst.Tables[0].Rows[i][10].ToString(),
    dtst.Tables[0].Rows[i][9].ToString()});
				this.trnsDetListView.Items.Add(nwItem);
				}
			this.obeyEvnts = true;
			}

		private void correctValsNavLbls(DataSet dtst)
			{
			long totlRecs = dtst.Tables[0].Rows.Count;
			if (this.totl_vals == Global.mnFrm.cmCde.Big_Val
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
				this.totl_vals = Global.get_Total_Transactions(
					this.searchForTextBox.Text, this.searchInComboBox.Text,this.my_org_id);
				this.is_last_val = true;
				this.updtValTotals();
				this.cur_vals_idx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
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
		}
	}