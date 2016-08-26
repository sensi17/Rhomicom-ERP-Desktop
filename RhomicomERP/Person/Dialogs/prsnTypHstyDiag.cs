using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BasicPersonData.Classes;

namespace BasicPersonData.Dialogs
{
  public partial class prsnTypHstyDiag : Form
  {
    public prsnTypHstyDiag()
    {
      InitializeComponent();
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

    public long prsnID = -1;
    private void prsnTypHstyDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      System.Windows.Forms.Application.DoEvents();
      loadPrsnTyps();
    }

    private void loadPrsnTyps()
    {
      DataSet dtst = Global.getAllPrsnTyps(this.prsnID);
      this.prsnTypListView.Items.Clear();
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][2].ToString(), dtst.Tables[0].Rows[i][3].ToString()
				, dtst.Tables[0].Rows[i][4].ToString(), dtst.Tables[0].Rows[i][5].ToString()});
        this.prsnTypListView.Items.Add(nwItem);
      }
    }

    private void rfrshTypMenuItem_Click(object sender, EventArgs e)
    {
      this.loadPrsnTyps();
    }

    private void rcHstryTypMenuItem_Click(object sender, EventArgs e)
    {
      if (this.prsnTypListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(
        long.Parse(this.prsnTypListView.SelectedItems[0].SubItems[6].Text), "pasn.prsn_prsntyps", "prsntype_id"), 6);
    }

    private void vwSQLTypMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(Global.mnFrm.prsntyp_SQL, 5);
    }

    private void deleteTypToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }
      if (this.prsnTypListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the record to DELETE!", 0);
        return;
      }
      string datestr = Global.mnFrm.cmCde.getDB_Date_time();
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
 "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.deletePrsTyp(long.Parse(this.prsnTypListView.SelectedItems[0].SubItems[6].Text),
        this.prsnTypListView.SelectedItems[0].SubItems[1].Text);

      //if (Global.isPrsnTypeActive(datestr, long.Parse(this.prsnTypListView.SelectedItems[0].SubItems[6].Text)) == false)
      //{
      //}
      //else
      //{
      //  Global.mnFrm.cmCde.showMsg("Cannot Delete an Active Person Type!", 0);
      //  return;
      //}
      this.loadPrsnTyps();
    }
  }
}