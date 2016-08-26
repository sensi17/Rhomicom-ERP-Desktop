using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemAdministration.Classes;

namespace SystemAdministration.Dialogs
	{
	public partial class editPlcyMdlsDiag : Form
		{
		public editPlcyMdlsDiag()
			{
			InitializeComponent();
			}

		public int plcyID = -1;
		public int mdlID = -1;
		public string actions_brght = "";
		private void enblTrknYesCheckBox_CheckedChanged(object sender, EventArgs e)
			{
			this.enblTrknNoCheckBox.Checked = !this.enblTrknYesCheckBox.Checked;
			}

		private void enblTrknNoCheckBox_CheckedChanged(object sender, EventArgs e)
			{
			this.enblTrknYesCheckBox.Checked = !this.enblTrknNoCheckBox.Checked;
			}

		private void okButton_Click(object sender, EventArgs e)
			{
			string nw_actions = "";
			for (int i = 0; i < actionsCheckedListBox.CheckedItems.Count; i++)
				{
				nw_actions += actionsCheckedListBox.CheckedItems[i].ToString() + ", ";
				}
			char[] trm = { ' ', ',' };
			if (Global.hasPlcyEvrHdThsMdl(this.plcyID, this.mdlID) == true)
				{
				Global.updateActnsToTrack(this.plcyID, this.mdlID,
					this.enblTrknYesCheckBox.Checked, nw_actions.Trim(trm));
				}
			else
				{
				Global.asgnMdlToPlcy(this.plcyID, this.mdlID,
					this.enblTrknYesCheckBox.Checked, nw_actions.Trim(trm));
				}
			this.DialogResult = DialogResult.OK;
			this.Close();
			}

		private void cancelButton_Click(object sender, EventArgs e)
			{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
			}

		private void editPlcyMdlsDiag_Load(object sender, EventArgs e)
			{
        System.Windows.Forms.Application.DoEvents();
        Color[] clrs = Global.myNwMainFrm.cmmnCode.getColors();
        this.BackColor = clrs[0];
        for (int i = 0; i < actionsCheckedListBox.Items.Count; i++)
				{
				if (this.actions_brght.Contains(actionsCheckedListBox.Items[i].ToString()))
					{
					this.actionsCheckedListBox.SetItemChecked(i, true);
					}
				}
			}
		}
	}