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
 public partial class addWkDetDiag : Form
  {
  public addWkDetDiag()
   {
   InitializeComponent();
   }

  private void okButton_Click(object sender, EventArgs e)
   {
   if (this.dayWeekComboBox.Text == "")
    {
    Global.mnFrm.cmCde.showMsg("Please select a Day of Week!", 0);
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

   private void addWkDetDiag_Load(object sender, EventArgs e)
   {
     System.Windows.Forms.Application.DoEvents();
     Color[] clrs = Global.mnFrm.cmCde.getColors();
     this.BackColor = clrs[0];
   }
  }
 }