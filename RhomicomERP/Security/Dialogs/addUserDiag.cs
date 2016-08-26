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
  public partial class addUserDiag : Form
  {
    public addUserDiag()
    {
      InitializeComponent();
    }

    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    private void usrDte1Button_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCode.selectDate(ref this.usrVldStrtDteTextBox);
    }

    private void usrDte2Button_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCode.selectDate(ref this.usrVldEndDteTextBox);
    }

    private void getPersonButton_Click(object sender, EventArgs e)
    {
      if (this.ownerTypComboBox.Text == "Person")
      {
        Global.selectPerson(ref this.ownerTextBox, ref this.prsnIDTextBox, this.srchWrd);
      }
      else
      {
        long cstspplID = long.Parse(this.prsnIDTextBox.Text);
        long siteID = -1;
        bool isReadOnly = false;

        Global.myNwMainFrm.cmmnCode.showCstSpplrDiag(ref cstspplID, ref siteID, true, false, this.srchWrd,
          "Customer/Supplier Name", true, isReadOnly, Global.myNwMainFrm.cmmnCode, "Customer");
        this.prsnIDTextBox.Text = cstspplID.ToString();
        this.ownerTextBox.Text = Global.myNwMainFrm.cmmnCode.getGnrlRecNm(
            "scm.scm_cstmr_suplr", "cust_sup_id", "cust_sup_name",
            cstspplID);
      }
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.uNameTextBox.Text == "" || this.prsnIDTextBox.Text == ""
        || this.prsnIDTextBox.Text == "-1")
      {
        MessageBox.Show("Please fill all required fields!", "Rhomicom Message!",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      if (Global.getUserID(this.uNameTextBox.Text) > 0 && this.uNameTextBox.ReadOnly == false)
      {
        MessageBox.Show("This user name is already in use!", "Rhomicom Message!",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      long prsnID = -1;
      long cstmrID = -1;
      if (this.ownerTypComboBox.Text == "Person")
      {
        prsnID = long.Parse(this.prsnIDTextBox.Text);
      }
      else
      {
        cstmrID = long.Parse(this.prsnIDTextBox.Text);
      }
      if (this.uNameTextBox.ReadOnly == false)
      {
        Global.createUser(this.uNameTextBox.Text, prsnID,
          this.usrVldStrtDteTextBox.Text, this.usrVldEndDteTextBox.Text,
        Global.generatePswd(), cstmrID);
        if (MessageBox.Show("User Saved Successfully! Want to create a new one?", "Rhomicom Message!",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
        {
          this.DialogResult = DialogResult.OK;
          this.Close();
        }
        else
        {
          this.renewPage();
        }
      }
      else
      {
        Global.updateUser(this.uNameTextBox.Text, prsnID,
    this.usrVldStrtDteTextBox.Text, this.usrVldEndDteTextBox.Text, cstmrID);
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
    }

    private void addUserDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmmnCode.getColors();
      this.BackColor = clrs[0];
      if (this.prsnIDTextBox.Text == "")
      {
        this.prsnIDTextBox.Text = "-1";
      }
      this.obey_evnts = true;
    }

    private void renewPage()
    {
      this.prsnIDTextBox.Text = "-1";
      this.uNameTextBox.Text = "";
      this.ownerTextBox.Text = "";
      this.usrVldEndDteTextBox.Text = "";
      this.usrVldStrtDteTextBox.Text = "";
    }
    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void ownerTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }

    private void ownerTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;
      this.obey_evnts = false;
      this.srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        this.srchWrd = "%" + this.srchWrd.Replace(" ", "%") + "%";
      }

      if (mytxt.Name == "ownerTextBox")
      {
        this.ownerTextBox.Text = "";
        this.prsnIDTextBox.Text = "-1";
        this.getPersonButton_Click(this.getPersonButton, e);
      }
      else if (mytxt.Name == "usrVldStrtDteTextBox")
      {
        this.usrVldStrtDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.usrVldStrtDteTextBox.Text);
      }
      else if (mytxt.Name == "usrVldEndDteTextBox")
      {
        this.usrVldEndDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.usrVldEndDteTextBox.Text);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void ownerTypComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.prsnIDTextBox.Text = "-1";
      this.ownerTextBox.Text = "";
    }
  }
}