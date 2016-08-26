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
  public partial class addRoleDiag : Form
  {
    public addRoleDiag()
    {
      InitializeComponent();
    }

    public int brght_role_id = -1;
    bool obey_evnts = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    private void roleDte1Button_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCode.selectDate(ref this.roleVldStrtDteTextBox);
    }

    private void roleDte2Button_Click(object sender, EventArgs e)
    {
      Global.myNwMainFrm.cmmnCode.selectDate(ref this.roleVldEndDteTextBox);
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.roleNameTextBox.Text == "")
      {
        MessageBox.Show("Please fill all required fields!", "Rhomicom Message!",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
      }
      if (Global.myNwMainFrm.cmmnCode.getRoleID(this.roleNameTextBox.Text) > 0 && this.brght_role_id <= 0)
      {
        MessageBox.Show("This role name is already in use!", "Rhomicom Message!",
          MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
      if (brght_role_id > 0)
      {
        Global.updateRole(this.brght_role_id, this.roleNameTextBox.Text,
          this.roleVldStrtDteTextBox.Text, this.roleVldEndDteTextBox.Text,
          this.checkBox1.Checked);
        this.DialogResult = DialogResult.OK;
        this.Close();
      }
      else
      {
        Global.createRole(this.roleNameTextBox.Text,
        this.roleVldStrtDteTextBox.Text, this.roleVldEndDteTextBox.Text,
          this.checkBox1.Checked);
        if (MessageBox.Show("Role Saved Successfully! Want to create a new one?", "Rhomicom Message!",
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
    }

    private void renewPage()
    {
      this.brght_role_id = -1;
      this.roleNameTextBox.Text = "";
      this.roleVldStrtDteTextBox.Text = "";
      this.roleVldEndDteTextBox.Text = "";
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void addRoleDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmmnCode.getColors();
      this.BackColor = clrs[0];
      this.obey_evnts = true;
    }

    private void roleVldStrtDteTextBox_Leave(object sender, EventArgs e)
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

      if (mytxt.Name == "roleVldStrtDteTextBox")
      {
        this.roleVldStrtDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.roleVldStrtDteTextBox.Text);
      }
      else if (mytxt.Name == "roleVldEndDteTextBox")
      {
        this.roleVldEndDteTextBox.Text = Global.myNwMainFrm.cmmnCode.checkNFormatDate(this.roleVldEndDteTextBox.Text);
      }
      this.srchWrd = "%";
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void roleVldStrtDteTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;
    }
  }
}