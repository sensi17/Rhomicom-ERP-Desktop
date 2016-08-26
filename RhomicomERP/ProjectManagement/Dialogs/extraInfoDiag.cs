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
  public partial class extraInfoDiag : Form
  {
    public extraInfoDiag()
    {
      InitializeComponent();
    }
    public long itmID = -1;
    private void extraInfoDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.tabPage1.BackColor = clrs[0];
      this.tabPage2.BackColor = clrs[0];
      this.subtabPageDrugInteractions.BackColor = clrs[0];
      this.subtabPageExtraLbls.BackColor = clrs[0];

      DataSet dtst = Global.get_ItemExtInf(this.itmID);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        this.extraInfoTextBox.Text = dtst.Tables[0].Rows[0][1].ToString();
        this.otherInfoTextBox.Text = dtst.Tables[0].Rows[0][2].ToString();

        this.genNametextBox.Text = dtst.Tables[0].Rows[0][3].ToString();
        this.tradeNametextBox.Text = dtst.Tables[0].Rows[0][4].ToString();
        this.usualDsgetextBox.Text = dtst.Tables[0].Rows[0][5].ToString();
        this.maxDsgetextBox.Text = dtst.Tables[0].Rows[0][6].ToString();
        this.contraindctntextBox.Text = dtst.Tables[0].Rows[0][7].ToString();
        this.foodInterctnstextBox.Text = dtst.Tables[0].Rows[0][8].ToString();

        Global.mnFrm.cmCde.getDBImageFile(
          dtst.Tables[0].Rows[0][0].ToString(), 3, ref this.prvwPictureBox);
      }

      this.drugIntrctnlistView.Items.Clear();

      string qrySelectDrugInteraction = @"SELECT row_number() over(order by b.item_code) as row , b.item_desc || '(' || b.item_code || ')', a.intrctn_effect,
          a.action, a.second_drug_id, a.drug_intrctn_id " +
          " FROM inv.inv_drug_interactions a inner join inv.inv_itm_list b ON a.first_drug_id = b.item_id " +
          " WHERE a.first_drug_id = " + this.itmID + " order by 1 ";

      DataSet Ds = new DataSet();

      Ds.Reset();

      //fill dataset
      Ds = Global.fillDataSetFxn(qrySelectDrugInteraction);

      int varMaxRows = Ds.Tables[0].Rows.Count;

      for (int i = 0; i < varMaxRows; i++)
      {
        //read data into array
        string[] colArray = {Ds.Tables[0].Rows[i][1].ToString(),  Ds.Tables[0].Rows[i][2].ToString(), Ds.Tables[0].Rows[i][3].ToString(), 
                    Ds.Tables[0].Rows[i][4].ToString(), Ds.Tables[0].Rows[i][5].ToString()};

        //add data to listview
        this.drugIntrctnlistView.Items.Add(Ds.Tables[0].Rows[i][0].ToString().ToString()).SubItems.AddRange(colArray);
      }
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.OK;
      this.Close();
    }
  }
}