using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using OrganizationSetup.Classes;

namespace OrganizationSetup.Dialogs
{
    public partial class addBalItmDiag : Form
    {
        public addBalItmDiag()
        {
            InitializeComponent();
        }
        public int orgID = -1;

        private void itmNameButton_Click(object sender, EventArgs e)
        {
            //Item Names
          if (onlyBalsItms)
          {
            string[] selVals = new string[1];
            selVals[0] = this.itemIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Balance Items"), ref selVals, true, true, this.orgID);
            if (dgRes == DialogResult.OK)
            {
              for (int i = 0; i < selVals.Length; i++)
              {
                this.itemIDTextBox.Text = selVals[i];
                this.itmNameTextBox.Text = Global.mnFrm.cmCde.getItmName(int.Parse(selVals[i]));
              }
            }
          }
          else
          {
            string[] selVals = new string[1];
            selVals[0] = this.itemIDTextBox.Text;
            DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                Global.mnFrm.cmCde.getLovID("Non-Balance Items"), ref selVals, 
                true, true, this.orgID);
            if (dgRes == DialogResult.OK)
            {
              for (int i = 0; i < selVals.Length; i++)
              {
                this.itemIDTextBox.Text = selVals[i];
                this.itmNameTextBox.Text = Global.mnFrm.cmCde.getItmName(int.Parse(selVals[i]));
              }
            }
          }
        }
      public bool onlyBalsItms = true;
        private void okButton_Click(object sender, EventArgs e)
        {
            if (this.itemIDTextBox.Text == "-1"
                || this.itemIDTextBox.Text == "")
            {
                Global.mnFrm.cmCde.showMsg("Please select an Item!", 0);
                return;
            }
            if (onlyBalsItms)
            {
              string usesSQL = Global.mnFrm.cmCde.getGnrlRecNm("org.org_pay_items",
      "item_id", "uses_sql_formulas", long.Parse(this.itemIDTextBox.Text));
              if (usesSQL == "1")
              {
                Global.mnFrm.cmCde.showMsg("Cannot Select a Balance Item whose \r\nBalance is Generated Dynamically!", 0);
                return;
              }
            }
            if (this.addSubComboBox.Text == "")
            {
              Global.mnFrm.cmCde.showMsg("Please select whether to add/Subtract!", 0);
              return;
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

      private void addBalItmDiag_Load(object sender, EventArgs e)
      {
        System.Windows.Forms.Application.DoEvents();
        Color[] clrs = Global.mnFrm.cmCde.getColors();
        this.BackColor = clrs[0];
      }
    }
}