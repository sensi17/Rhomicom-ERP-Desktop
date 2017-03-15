using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using InternalPayments.Classes;

namespace InternalPayments.Forms
{
  public partial class addAttchmntDiag : Form
  {
    public addAttchmntDiag()
    {
      InitializeComponent();
    }
    public long batchID = -1;
    public bool isPrchSng = false;
    public long attchCtgry = 0;

    private void baseDirButton_Click(object sender, EventArgs e)
    {
      this.fileNmTextBox.Text = Global3.mnFrm.cmCde1.pickAFile();
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      if (this.attchmntNmTextBox.Text == "")
      {
        Global3.mnFrm.cmCde1.showMsg("Please provide a name/description for the File!", 0);
        return;
      }
      if (this.fileNmTextBox.Text == "")
      {
        Global3.mnFrm.cmCde1.showMsg("Please select the File to Add!", 0);
        return;
      }
      if (Global3.mnFrm.cmCde1.myComputer.FileSystem.FileExists(this.fileNmTextBox.Text) == false)
      {
        Global3.mnFrm.cmCde1.showMsg("Please select a valid File!", 0);
        return;
      }
      long oldattchID = -1;
      if (this.attchCtgry == 3)
      {
        oldattchID = Global3.getAttchmntID(this.attchmntNmTextBox.Text,
          this.batchID, "accb.accb_pybl_doc_attchmnts", "doc_hdr_id");
      }
      else if (this.isPrchSng == false)
      {
        oldattchID = Global3.getAttchmntID(this.attchmntNmTextBox.Text,
          this.batchID);

      }
      else
      {
        oldattchID = Global3.getP_AttchmntID(this.attchmntNmTextBox.Text,
             this.batchID);
      }
      if (oldattchID > 0
 && this.attchmntIDTextBox.Text == "-1")
      {
        Global3.mnFrm.cmCde1.showMsg("Attachment Name is already in use in this Batch!", 0);
        return;
      }
      else if (oldattchID > 0
 && oldattchID.ToString() !=
 this.attchmntIDTextBox.Text)
      {
        Global3.mnFrm.cmCde1.showMsg("New Attachment Name is already in use in this Batch!", 0);
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

    private void addAttchmntDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global3.mnFrm.cmCde1.getColors();
      this.BackColor = clrs[0];
    }

    private void docCtgryButton_Click(object sender, EventArgs e)
    {
      //Attachment Document Categories
      int[] selVals = new int[1];
      selVals[0] = Global3.mnFrm.cmCde1.getPssblValID(this.attchmntNmTextBox.Text,
        Global3.mnFrm.cmCde1.getLovID("Attachment Document Categories"));
      DialogResult dgRes = Global3.mnFrm.cmCde1.showPssblValDiag(
       Global3.mnFrm.cmCde1.getLovID("Attachment Document Categories"), ref selVals, true, true,
       "%", "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.attchmntNmTextBox.Text = Global3.mnFrm.cmCde1.getPssblValNm(selVals[i]);
        }
      }
    }
  }
}