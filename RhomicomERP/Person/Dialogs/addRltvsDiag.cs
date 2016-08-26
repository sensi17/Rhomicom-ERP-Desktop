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
 public partial class addRltvsDiag : Form
  {
  public addRltvsDiag()
   {
   InitializeComponent();
   }
  public long rltv_id = -1;
  private void addRltvsDiag_Load(object sender, EventArgs e)
   {
     System.Windows.Forms.Application.DoEvents();
     Color[] clrs = Global.mnFrm.cmCde.getColors();
     this.BackColor = clrs[0];
   }

  private void idNoButton_Click(object sender, EventArgs e)
   {
   //Active Persons
   string[] selVals = new string[1];
   selVals[0] = this.idNoTextBox.Text;
   DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
    Global.mnFrm.cmCde.getLovID("Active Persons"), ref selVals, true, true);
   if (dgRes == DialogResult.OK)
    {
    for (int i = 0; i < selVals.Length; i++)
     {
     this.idNoTextBox.Text = selVals[i];
     this.rltvNameTextBox.Text = Global.mnFrm.cmCde.getPrsnName(selVals[i]);     
     }
    }
   }

  private void rltnTypButton_Click(object sender, EventArgs e)
   {
   //Relationship Types
   int[] selVals = new int[1];
   selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.rltnTypTextBox.Text,
    Global.mnFrm.cmCde.getLovID("Relationship Types"));
   DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
    Global.mnFrm.cmCde.getLovID("Relationship Types"), ref selVals, true, false);
   if (dgRes == DialogResult.OK)
    {
    for (int i = 0; i < selVals.Length; i++)
     {
     this.rltnTypTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
     }
    }
   }

  private void okButton_Click(object sender, EventArgs e)
   {
   if (this.idNoTextBox.Text == "" || this.rltnTypTextBox.Text == "")
    {
    Global.mnFrm.cmCde.showMsg("Please fill all required fields!", 0);
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
  }
 }