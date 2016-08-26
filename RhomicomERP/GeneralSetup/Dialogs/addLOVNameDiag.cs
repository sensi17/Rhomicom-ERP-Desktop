using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using GeneralSetup.Classes;

namespace GeneralSetup.Dialogs
	{
	public partial class addLOVNameDiag : Form
		{
		public addLOVNameDiag()
			{
			InitializeComponent();
			}

		private void addLOVNameDiag_Load(object sender, EventArgs e)
			{
        System.Windows.Forms.Application.DoEvents();
        Color[] clrs = Global.myNwMainFrm.cmmnCodeGstp.getColors();
        this.BackColor = clrs[0];
      }

		private void isDynmcVlNmCheckBox_CheckedChanged(object sender, EventArgs e)
			{
			if (this.isDynmcVlNmCheckBox.Checked == true)
				{
				this.sqlQueryTextBox.ReadOnly = false;
				}
			else
				{
				this.sqlQueryTextBox.Text = "";
				this.sqlQueryTextBox.ReadOnly = true;
				}
			}

		private void okButton_Click(object sender, EventArgs e)
			{
			if (this.lovNameTextBox.Text == ""
        || this.definedByComboBox.Text == ""
        || this.orderByTextBox.Text == "")
				{
				Global.myNwMainFrm.cmmnCodeGstp.showMsg("Please fill all required fields!", 0);
				return;
				}
			if (this.isDynmcVlNmCheckBox.Checked==true
				&& this.sqlQueryTextBox.Text == "")
				{
				Global.myNwMainFrm.cmmnCodeGstp.showMsg("A Valid SQL Statement is required for dynamic LOVs!", 0);
				return;
				}
			if (this.isDynmcVlNmCheckBox.Checked == true
				&& this.sqlQueryTextBox.Text != "")
				{
				//if (this.sqlQueryTextBox.Text.Contains(" a,") || this.sqlQueryTextBox.Text.Contains(" b,")
				// || this.sqlQueryTextBox.Text.Contains(" c,"))
				// {
				// Global.myNwMainFrm.cmmnCodeGstp.showMsg("Custom queries", 0);
				// return;
				// }
				try
					{
					Global.myNwMainFrm.cmmnCodeGstp.selectDataNoParams(this.sqlQueryTextBox.Text);
					}
				catch (Exception ex)
					{
					Global.myNwMainFrm.cmmnCodeGstp.showMsg(ex.Message + 
						"\r\nA Valid SQL Statement is required for dynamic LOVs!", 0);
					return;
					}
				}
			if (this.lovIDTextBox.Text == "-1" || this.lovIDTextBox.Text == "")
				{
				if (Global.getLovID(this.lovNameTextBox.Text) > 0)
					{
					Global.myNwMainFrm.cmmnCodeGstp.showMsg("Value List Name is already in use!", 0);
					return;
					}
				Global.createLovNm(this.lovNameTextBox.Text, this.descVlNmTextBox.Text,
							this.isDynmcVlNmCheckBox.Checked, this.sqlQueryTextBox.Text,
              this.definedByComboBox.Text, this.isEnbldVlNmCheckBox.Checked, this.orderByTextBox.Text);
				if (Global.myNwMainFrm.cmmnCodeGstp.showMsg("Value List Name Saved Successfully!" + 
					"\r\nDo you want to create another one?", 2) == DialogResult.Yes)
					{
					this.lovIDTextBox.Text = "-1";
					this.lovNameTextBox.Text = "";
					this.isEnbldVlNmCheckBox.Checked = false;
					this.isDynmcVlNmCheckBox.Checked = false;
					this.descVlNmTextBox.Text = "";
					this.sqlQueryTextBox.Text = "";
					this.definedByComboBox.SelectedItem = "USR";
          this.orderByTextBox.Text = "ORDER BY 1";
          }
				else
					{
					this.DialogResult = DialogResult.OK;
					this.Close();
					}
				}
			else
				{
				if (Global.getLovID(this.lovNameTextBox.Text) != int.Parse(this.lovIDTextBox.Text))
					{
					if (Global.getLovID(this.lovNameTextBox.Text) > 0)
						{
						Global.myNwMainFrm.cmmnCodeGstp.showMsg("New Value List Name is already in use!", 0);
						return;
						}
					}
				Global.updateLovNm(int.Parse(this.lovIDTextBox.Text), this.lovNameTextBox.Text, this.descVlNmTextBox.Text,
			this.isDynmcVlNmCheckBox.Checked, this.sqlQueryTextBox.Text,
      this.definedByComboBox.Text, this.isEnbldVlNmCheckBox.Checked, this.orderByTextBox.Text);
				this.DialogResult = DialogResult.OK;
				this.Close();
				}
			}

		private void cancelButton_Click(object sender, EventArgs e)
			{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
			}
		}
	}