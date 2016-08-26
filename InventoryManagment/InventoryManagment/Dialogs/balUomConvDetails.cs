using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;

namespace StoresAndInventoryManager.Forms
{
    public partial class balUomConvDetails : Form
    {
        public balUomConvDetails()
        {
            InitializeComponent();
        }

        #region "VARIABLES"
        DataGridViewRow row = null;
        public static string varUomQtyRcvd;
        #endregion

        #region "LOCAL FUNCTIONS"
        public string ttlTxt
        {
            get { return ttlQtytextBox.Text; }
            set { ttlQtytextBox.Text = value; }
        }

        public string rsvdTxt
        {
            get { return rsvdQtytextBox.Text; }
            set { rsvdQtytextBox.Text = value; }
        }

        public string avlblTxt
        {
            get { return avlblQtytextBox.Text; }
            set { avlblQtytextBox.Text = value; }
        }

        //public string avlblTxt
        //{
        //    get { return balUomConvDetails.Text; }
        //    set { balUomConvDetails.Text = value; }
        //}

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

        public void populateViewUomConversionGridView(string uomItemCode, string varTtlQty, string varRsvdQty, string varAvaiableQty)
        {
            double cnvsnFctr = 0;
            double ttlQty = 0;
            double ttlWhlPrt = 0;
            double ttlRmndPrt = 0;
            double ttlWhlPrtVal = 0;
            double ttlRmndPrtVal = 0;
            double ttlCnvrtdQty = 0;
            double ttlRngSum = 0;
            double rsvdQty = 0;
            double rsvdWhlPrt = 0;
            double rsvdRmndPrt = 0;
            double rsvdWhlPrtVal = 0;
            double rsvdRmndPrtVal = 0;
            double rsvdCnvrtdQty = 0;
            double rsvdRngSum = 0;
            double avlblQty = 0;
            double avlblWhlPrt = 0;
            double avlblRmndPrt = 0;
            double avlblWhlPrtVal = 0;
            double avlblRmndPrtVal = 0;
            double avlblCnvrtdQty = 0;
            double avlblRngSum = 0;


            ttlQty = double.Parse(varTtlQty);
            ttlRmndPrtVal = ttlQty;
            rsvdQty = double.Parse(varRsvdQty);
            rsvdRmndPrtVal = rsvdQty;
            avlblQty = double.Parse(varAvaiableQty);
            avlblRmndPrtVal = avlblQty;

            //clear datagridview
            dataGridViewUomConversion.AutoGenerateColumns = false;

            dataGridViewUomConversion.Rows.Clear();

            if (uomItemCode != "")
            {
                string qrySelectDetInfo = "SELECT a.itm_uom_id mt, (SELECT b.uom_name FROM inv.unit_of_measure b WHERE b.uom_id = a.uom_id) uom, " +
                    " a.uom_id mt, uom_level mt, cnvsn_factor mt " +
                    " FROM inv.itm_uoms a WHERE a.item_id = (SELECT item_id FROM inv.inv_itm_list WHERE item_code = " +
                    " '" + uomItemCode.Replace("'", "''") + "') " +
                    " union " +
                    " SELECT -1 mt, (SELECT b.uom_name FROM inv.unit_of_measure b WHERE b.uom_id = a.base_uom_id) uom, " +
                    " base_uom_id mt, -1 mt, 1 mt " +
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
                        
                        //TOTAL QTY COVERSION
                        if (ttlRngSum == ttlQty)
                        {
                            ttlCnvrtdQty = 0;
                            ttlWhlPrtVal = 0;
                        }
                        else
                        {
                            if (ttlRmndPrtVal >= cnvsnFctr)
                            {
                                ttlWhlPrt = (int)(ttlRmndPrtVal / cnvsnFctr);
                                ttlRmndPrt = ttlRmndPrtVal % cnvsnFctr;

                                if (ttlWhlPrt > 0)
                                {
                                    ttlWhlPrtVal = ttlWhlPrt;
                                    ttlCnvrtdQty = ttlWhlPrtVal * cnvsnFctr;
                                }

                                if (ttlRmndPrt > 0)
                                {
                                    ttlRmndPrtVal = ttlRmndPrt;
                                }
                            }
                            else
                            {
                                ttlCnvrtdQty = 0;
                                ttlWhlPrtVal = 0;
                            }

                            ttlRngSum = ttlRngSum + ttlCnvrtdQty;
                        }

                        //RSVD QTY COVERSION
                        if (rsvdRngSum == rsvdQty)
                        {
                            rsvdCnvrtdQty = 0;
                            rsvdWhlPrtVal = 0;
                        }
                        else
                        {
                            if (rsvdRmndPrtVal >= cnvsnFctr)
                            {
                                rsvdWhlPrt = (int)(rsvdRmndPrtVal / cnvsnFctr);
                                rsvdRmndPrt = rsvdRmndPrtVal % cnvsnFctr;

                                if (rsvdWhlPrt > 0)
                                {
                                    rsvdWhlPrtVal = rsvdWhlPrt;
                                    rsvdCnvrtdQty = rsvdWhlPrtVal * cnvsnFctr;
                                }

                                if (rsvdRmndPrt > 0)
                                {
                                    rsvdRmndPrtVal = rsvdRmndPrt;
                                }
                            }
                            else
                            {
                                rsvdCnvrtdQty = 0;
                                rsvdWhlPrtVal = 0;
                            }

                            rsvdRngSum = rsvdRngSum + rsvdCnvrtdQty;
                        }

                        //RESERVED QTY COVERSION
                        if (avlblRngSum == avlblQty)
                        {
                            avlblCnvrtdQty = 0;
                            avlblWhlPrtVal = 0;
                        }
                        else
                        {
                            if (avlblRmndPrtVal >= cnvsnFctr)
                            {
                                avlblWhlPrt = (int)(avlblRmndPrtVal / cnvsnFctr);
                                avlblRmndPrt = avlblRmndPrtVal % cnvsnFctr;

                                if (avlblWhlPrt > 0)
                                {
                                    avlblWhlPrtVal = avlblWhlPrt;
                                    avlblCnvrtdQty = avlblWhlPrtVal * cnvsnFctr;
                                }

                                if (avlblRmndPrt > 0)
                                {
                                    avlblRmndPrtVal = avlblRmndPrt;
                                }
                            }
                            else
                            {
                                avlblCnvrtdQty = 0;
                                avlblWhlPrtVal = 0;
                            }

                            avlblRngSum = avlblRngSum + avlblCnvrtdQty;
                        }

                        row = new DataGridViewRow();

                        DataGridViewCell detListNoCell = new DataGridViewTextBoxCell();
                        detListNoCell.Value = (i + 1).ToString();
                        row.Cells.Add(detListNoCell);

                        DataGridViewCell detUomCell = new DataGridViewTextBoxCell();
                        detUomCell.Value = newDs.Tables[0].Rows[i][1].ToString();
                        row.Cells.Add(detUomCell);

                        DataGridViewCell detTtlQtyCell = new DataGridViewTextBoxCell();
                        //detQtyCell.Value = "0";
                        detTtlQtyCell.Value = ttlWhlPrtVal;
                        row.Cells.Add(detTtlQtyCell);

                        DataGridViewCell detTtlEquivQtyCell = new DataGridViewTextBoxCell();
                        //detEquivQtyCell.Value = "0";
                        detTtlEquivQtyCell.Value = ttlCnvrtdQty;
                        row.Cells.Add(detTtlEquivQtyCell);

                        DataGridViewCell detRsvdQtyCell = new DataGridViewTextBoxCell();
                        //detQtyCell.Value = "0";
                        detRsvdQtyCell.Value = rsvdWhlPrtVal;
                        row.Cells.Add(detRsvdQtyCell);

                        DataGridViewCell detRsvdEquivQtyCell = new DataGridViewTextBoxCell();
                        //detEquivQtyCell.Value = "0";
                        detRsvdEquivQtyCell.Value = rsvdCnvrtdQty;
                        row.Cells.Add(detRsvdEquivQtyCell);

                        DataGridViewCell detAvlblQtyCell = new DataGridViewTextBoxCell();
                        //detQtyCell.Value = "0";
                        detAvlblQtyCell.Value = avlblWhlPrtVal;
                        row.Cells.Add(detAvlblQtyCell);

                        DataGridViewCell detAvlblEquivQtyCell = new DataGridViewTextBoxCell();
                        //detEquivQtyCell.Value = "0";
                        detAvlblEquivQtyCell.Value = avlblCnvrtdQty;
                        row.Cells.Add(detAvlblEquivQtyCell);

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

                        dataGridViewUomConversion.Rows.Add(row);

                    }
                }

                //this.ttlQtytextBox.Text = varTtlQty;
            }

        }
        #endregion

        #region "EVENTS"
        private void uomCancelbutton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion
    }
}
