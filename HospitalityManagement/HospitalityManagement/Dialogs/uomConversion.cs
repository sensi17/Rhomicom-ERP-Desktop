using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using HospitalityManagement.Classes;

namespace HospitalityManagement.Forms
{
  public partial class uomConversion : Form
  {
    public uomConversion()
    {
      InitializeComponent();
    }

    #region "VARIABLES"
    DataGridViewRow row = null;
    public static string varUomQtyRcvd;
    #endregion

    #region "FUNCTIONS"
    public string ttlTxt
    {
      get { return ttlQtytextBox.Text; }
      set { ttlQtytextBox.Text = value; }
    }

    public string cntrlTxt
    {
      get { return cntrltextBox.Text; }
      set { cntrltextBox.Text = value; }
    }

    public static int getItmUomCount(string uomItemCode)
    {
      string strSql = "SELECT a.itm_uom_id mt, (SELECT b.uom_name FROM inv.unit_of_measure b WHERE b.uom_id = a.uom_id) uom, " +
              " a.uom_id mt, uom_level mt, cnvsn_factor mt " +
              " FROM inv.itm_uoms a WHERE a.item_id = (SELECT item_id FROM inv.inv_itm_list WHERE item_code = " +
              " '" + uomItemCode.Replace("'", "''") + "') " +
              " union " +
              " SELECT -1 mt, (SELECT b.uom_name FROM inv.unit_of_measure b WHERE b.uom_id = a.base_uom_id) uom, " +
              " base_uom_id mt, -1 mt, 1 mt " +
              " FROM inv.inv_itm_list a WHERE a.item_code = '" + uomItemCode.Replace("'", "''") + "' ORDER BY 4 DESC";
      //echo $strSql;
      DataSet dtst = Global.mnFrm.cmCde.selectDataNoParams(strSql);
      if (dtst.Tables[0].Rows.Count > 0)
      {
        return int.Parse(dtst.Tables[0].Rows[0][0].ToString());
      }
      else
      {
        return 0;
      }
    }

    public void populateViewUomConversionGridView(string uomItemCode, string varTtlQty, string parMode)
    {
      double ttlQty = 0;
      double whlPrt = 0;
      double rmndPrt = 0;
      double whlPrtVal = 0;
      double rmndPrtVal = 0;
      double cnvsnFctr = 0;
      double cnvrtdQty = 0;
      double rngSum = 0;
      double ttlPrce = 0;
      if (parMode == "Read")
      {
        submitCnvrtdQtybutton.Enabled = false;
        dataGridViewUomConversion.Columns[dataGridViewUomConversion.Columns.IndexOf(detQty)].ReadOnly = true;
      }
      else
      {
        submitCnvrtdQtybutton.Enabled = true;
        dataGridViewUomConversion.Columns[dataGridViewUomConversion.Columns.IndexOf(detQty)].ReadOnly = false;
      }


      ttlQty = double.Parse(varTtlQty);
      rmndPrtVal = ttlQty;


      //clear datagridview
      dataGridViewUomConversion.AutoGenerateColumns = false;

      dataGridViewUomConversion.Rows.Clear();

      if (uomItemCode != "")
      {
        string qrySelectDetInfo = "SELECT a.itm_uom_id mt, (SELECT b.uom_name FROM inv.unit_of_measure b WHERE b.uom_id = a.uom_id) uom, " +
            " a.uom_id mt, uom_level mt, cnvsn_factor mt, selling_price, price_less_tax " +
            " FROM inv.itm_uoms a WHERE a.item_id = (SELECT item_id FROM inv.inv_itm_list WHERE item_code = " +
            " '" + uomItemCode.Replace("'", "''") + "') " +
            " union " +
            " SELECT -1 mt, (SELECT b.uom_name FROM inv.unit_of_measure b WHERE b.uom_id = a.base_uom_id) uom, " +
            " base_uom_id mt, -1 mt, 1 mt, selling_price, orgnl_selling_price " +
            " FROM inv.inv_itm_list a WHERE a.item_code = '" + uomItemCode.Replace("'", "''") + "' ORDER BY 4 DESC";

        DataSet newDs = new DataSet();

        newDs.Reset();

        //fill dataset
        newDs = Global.fillDataSetFxn(qrySelectDetInfo);

        if (newDs.Tables[0].Rows.Count > 0)
        {
          for (int i = 0; i < newDs.Tables[0].Rows.Count; i++)
          {

            cnvsnFctr = double.Parse(newDs.Tables[0].Rows[i][4].ToString());

            if (rngSum == ttlQty)
            {
              cnvrtdQty = 0;
              whlPrtVal = 0;
            }
            else
            {
              if (rmndPrtVal >= cnvsnFctr)
              {
                whlPrt = (int)(rmndPrtVal / cnvsnFctr);
                rmndPrt = rmndPrtVal % cnvsnFctr;

                if (whlPrt > 0)
                {
                  whlPrtVal = whlPrt;
                  cnvrtdQty = whlPrtVal * cnvsnFctr;
                }

                if (rmndPrt > 0)
                {
                  rmndPrtVal = rmndPrt;
                }
              }
              else
              {
                cnvrtdQty = 0;
                whlPrtVal = 0;
              }

              rngSum = rngSum + cnvrtdQty;
            }

            row = new DataGridViewRow();

            DataGridViewCell detListNoCell = new DataGridViewTextBoxCell();
            detListNoCell.Value = (i + 1).ToString();
            row.Cells.Add(detListNoCell);

            DataGridViewCell detUomCell = new DataGridViewTextBoxCell();
            detUomCell.Value = newDs.Tables[0].Rows[i][1].ToString();
            row.Cells.Add(detUomCell);

            DataGridViewCell detQtyCell = new DataGridViewTextBoxCell();
            //detQtyCell.Value = "0";
            detQtyCell.Value = whlPrtVal;
            row.Cells.Add(detQtyCell);

            DataGridViewCell detEquivQtyCell = new DataGridViewTextBoxCell();
            //detEquivQtyCell.Value = "0";
            detEquivQtyCell.Value = cnvrtdQty;
            row.Cells.Add(detEquivQtyCell);

            DataGridViewCell detItmUomIdCell = new DataGridViewTextBoxCell();
            detItmUomIdCell.Value = newDs.Tables[0].Rows[i][2].ToString();
            row.Cells.Add(detItmUomIdCell);

            DataGridViewCell detUomIdCell = new DataGridViewTextBoxCell();
            detUomIdCell.Value = newDs.Tables[0].Rows[i][0].ToString();
            row.Cells.Add(detUomIdCell);

            DataGridViewCell detSortOrderCell = new DataGridViewTextBoxCell();
            detSortOrderCell.Value = newDs.Tables[0].Rows[i][3].ToString();
            row.Cells.Add(detSortOrderCell);

            DataGridViewCell detCnvsnFactorCell = new DataGridViewTextBoxCell();
            detCnvsnFactorCell.Value = newDs.Tables[0].Rows[i][4].ToString();
            row.Cells.Add(detCnvsnFactorCell);

            DataGridViewCell detSPCell = new DataGridViewTextBoxCell();
            detSPCell.Value = newDs.Tables[0].Rows[i][5].ToString();
            row.Cells.Add(detSPCell);

            DataGridViewCell detOrgnlPriceCell = new DataGridViewTextBoxCell();
            detOrgnlPriceCell.Value = newDs.Tables[0].Rows[i][6].ToString();
            row.Cells.Add(detOrgnlPriceCell);

            DataGridViewCell detTtlPriceCell = new DataGridViewTextBoxCell();
            detTtlPriceCell.Value = (whlPrtVal * double.Parse(newDs.Tables[0].Rows[i][5].ToString())).ToString("#,##0.00");
            row.Cells.Add(detTtlPriceCell);

            ttlPrce += whlPrtVal * double.Parse(newDs.Tables[0].Rows[i][5].ToString());
            dataGridViewUomConversion.Rows.Add(row);

          }
        }

        //this.ttlQtytextBox.Text = varTtlQty;
      }
      this.ttlPriceTextBox.Text = ttlPrce.ToString("#,##0.00");
      this.ttlPriceTextBox.ReadOnly = true;
    }
    #endregion

    #region "EVENTS"
    private void dataGridViewUomConversion_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (cntrltextBox.Text == "0")
      {
        if (dataGridViewUomConversion.Rows[e.RowIndex].Cells["detQty"].Value != null)
        {
          double qty;
          double ttlQty = 0;
          double ttlPrice = 0;

          if (double.TryParse(dataGridViewUomConversion.Rows[e.RowIndex].Cells["detQty"].Value.ToString(), out qty))
          {
            dataGridViewUomConversion.Rows[e.RowIndex].Cells["detEqvBaseQty"].Value =
                qty * double.Parse(dataGridViewUomConversion.Rows[e.RowIndex].Cells["detCnvsnFactor"].Value.ToString());
          }
          else
          {
            dataGridViewUomConversion.Rows[e.RowIndex].Cells["detEqvBaseQty"].Value = 0;
            dataGridViewUomConversion.Rows[e.RowIndex].Cells["detQty"].Value = 0;
          }


          foreach (DataGridViewRow gridrow in dataGridViewUomConversion.Rows)
          {
            if (gridrow.Cells["detQty"].Value != null)
            {
              ttlPrice += double.Parse(gridrow.Cells[2].Value.ToString()) * double.Parse(gridrow.Cells[8].Value.ToString());
              gridrow.Cells[10].Value = double.Parse(gridrow.Cells[2].Value.ToString()) * double.Parse(gridrow.Cells[8].Value.ToString());
              ttlQty += double.Parse(gridrow.Cells[dataGridViewUomConversion.Columns.IndexOf(detEqvBaseQty)].Value.ToString());
            }
          }

          this.ttlQtytextBox.Text = ttlQty.ToString();
          this.ttlPriceTextBox.Text = ttlPrice.ToString("#,##0.00");
        }
        else
        {
          dataGridViewUomConversion.Rows[e.RowIndex].Cells["detQty"].Value = 0;
          dataGridViewUomConversion.Rows[e.RowIndex].Cells["detEqvBaseQty"].Value = 0;
        }
      }
    }

    private void uomCancelbutton_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void submitCnvrtdQtybutton_Click(object sender, EventArgs e)
    {
      try
      {
        varUomQtyRcvd = this.ttlQtytextBox.Text;

        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      catch (Exception ex)
      {
        Global.mnFrm.cmCde.showMsg(ex.Message, 0);
        return;
      }
    }
    #endregion

    private void uomConversion_Load(object sender, EventArgs e)
    {
      this.BackColor = Global.mnFrm.cmCde.getColors()[0];
    }
  }
}
