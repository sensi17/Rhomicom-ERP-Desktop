using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CommonCode;
using Npgsql;

namespace CommonCode
{
  public partial class activateDiag : Form
  {
    public activateDiag()
    {
      InitializeComponent();
    }
    CommonCodes cmnCde = new CommonCodes();
    //public NpgsqlConnection con;
    public bool actvated = false;

    private void activateDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs =cmnCde.getColors();
      this.BackColor = clrs[0]; 
      this.button1.Enabled = false;
      this.button1.Visible = false;
      //cmnCde.pgSqlConn = con;
      if (CommonCodes.GlobalSQLConn.State != ConnectionState.Open)
      {
        CommonCodes.GlobalSQLConn.Open();
      }
      this.rqstCodeTextBox.Text = cmnCde.getRequestCode();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      return;
      if (CommonCodes.GlobalSQLConn.State != ConnectionState.Open)
      {
        CommonCodes.GlobalSQLConn.Open();
      }
      this.actvateTextBox.Text = cmnCde.getExpctdActvtnKey(this.rqstCodeTextBox.Text);
    }

    private void OKButton_Click(object sender, EventArgs e)
    {
      if (CommonCodes.GlobalSQLConn.State != ConnectionState.Open)
      {
        CommonCodes.GlobalSQLConn.Open();
      }
      if (cmnCde.getExpctdActvtnKey(this.rqstCodeTextBox.Text) == this.actvateTextBox.Text)
      {
        cmnCde.writeValToRegstry("RHO_KEY", this.actvateTextBox.Text);
        cmnCde.showMsg("Sucessfully activated Product!", 3);
        this.actvated = true;
      }
      else
      {
        cmnCde.showMsg("Invalid Activation Key!", 4);
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