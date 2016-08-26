using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjectManagement.Classes;

namespace ProjectManagement.Dialogs
{
  public partial class attnScoresDiag : Form
  {
    public attnScoresDiag()
    {
      InitializeComponent();
    }
    public long recLineID = -1;
    public long tmtblDetID = -1;
    bool obeyEvnts = false;
    public bool rdOnly = true;

    private void attnScoresDiag_Load(object sender, EventArgs e)
    {
      //Labels for Attendance Points
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.obeyEvnts = false;
      this.populateValGridVw();
      this.extInfoDataGridView.ReadOnly = this.rdOnly;
      this.obeyEvnts = true;
    }

    private void populateValGridVw()
    {
      this.obeyEvnts = false;
      string lovNm = "Labels for Attendance Points";
      lovNm = Global.getEvntPointsLovNm(this.tmtblDetID);
      if (lovNm == "")
      {
        this.extInfoDataGridView.Rows.Clear();
        return;
      }
      DataSet dtst = Global.getStaticLOVValues(lovNm);

      this.extInfoDataGridView.Rows.Clear();
      this.extInfoDataGridView.RowCount = dtst.Tables[0].Rows.Count;
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        if (i > 9)
        {
          break;
        }
        //this.last_vals_num = cmnCde.navFuncts.startIndex() + i;
        this.extInfoDataGridView.Rows[i].HeaderCell.Value = (1 + i).ToString();
        Object[] cellDesc = new Object[4];
        cellDesc[0] = dtst.Tables[0].Rows[i][0].ToString();
        cellDesc[1] = Global.getPointScored((i + 1), this.recLineID);
        cellDesc[2] = this.recLineID;
        cellDesc[3] = (i + 1);
        this.extInfoDataGridView.Rows[i].SetValues(cellDesc);
      }
      //this.correctValsNavLbls(dtst);
      this.obeyEvnts = true;
      if (this.extInfoDataGridView.Rows.Count > 0)
      {
        this.extInfoDataGridView.Rows[0].Selected = true;
      }
      this.obeyEvnts = true;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void extInfoDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (e == null || this.obeyEvnts == false || (this.rdOnly == true))
      {
        return;
      }
      if (e.RowIndex < 0 || e.ColumnIndex < 0)
      {
        return;
      }
      bool prv = this.obeyEvnts;
      this.obeyEvnts = false;

      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[0].Value = string.Empty;
      }
      if (this.extInfoDataGridView.Rows[e.RowIndex].Cells[1].Value == null)
      {
        this.extInfoDataGridView.Rows[e.RowIndex].Cells[1].Value = "0";
      }
      if (e.ColumnIndex == 1)
      {
        int colNo = int.Parse(this.extInfoDataGridView.Rows[e.RowIndex].Cells[3].Value.ToString());
        double pntval = 0;
        //Global.com
        if (double.TryParse(this.extInfoDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), out pntval))
        {
          Global.updtPointScored(colNo, this.recLineID, pntval);
        }
        else
        {
          Global.mnFrm.cmCde.showMsg("Invalid Number", 0);
        }
      }

      this.obeyEvnts = prv;
    }

  }
}
