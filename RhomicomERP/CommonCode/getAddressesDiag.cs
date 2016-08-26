using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.DirectoryServices;
//using BasicPersonData.Classes;

namespace CommonCode
{
  public partial class getAddressesDiag : Form
  {
    public getAddressesDiag()
    {
      InitializeComponent();
    }

    private bool obeyChnge = false;
    public int whoCalld = 0;
    public CommonCodes cmnCde = new CommonCodes();

    private void getAddressesDiag_Load(object sender, EventArgs e)
    {
      Color[] clrs = cmnCde.getColors();
      this.BackColor = clrs[0];

      string selSql = "SELECT actv_drctry_domain_name FROM sec.sec_email_servers WHERE ((is_default = 't'))";
      DataSet selDtSt = cmnCde.selectDataNoParams(selSql);
      int m = selDtSt.Tables[0].Rows.Count;
      if (m > 0)
      {
        this.domainTextBox.Text = selDtSt.Tables[0].Rows[0][0].ToString();
      }
    }

    private void searchButton_Click(object sender, EventArgs e)
    {
      try
      {
        this.obeyChnge = false;
        StringBuilder buffer = new StringBuilder();
        DirectoryEntry entry = new DirectoryEntry("LDAP://" + this.domainTextBox.Text);
        DirectorySearcher dSearch = new DirectorySearcher(entry);
        dSearch.SizeLimit = 20000;
        string Name = this.fnameTextBox.Text;
        string sname = this.snameTextBox.Text;
        dSearch.Filter = "(&(objectCategory=person)(objectClass=*)(mail=*)(cn=" + Name + "*)(sn=" + sname + "*))";// "(&(objectClass=user))";
        //"(&(objectClass=user)(cn=" + Name + "))";//"(objectClass=user)";//"(&(objectClass=publicfolder))";
        this.resListView.Items.Clear();
        int i = 0;
        foreach (SearchResult sResultSet in dSearch.FindAll())
        {
          if (sResultSet.Properties["cn"].Count > 0)
          {
            i++;
            ListViewItem nwItem = new ListViewItem(new string[] { (i).ToString(), sResultSet.Properties["cn"][0].ToString(), sResultSet.Properties["mail"][0].ToString() });
            int rem = 0;
            Math.DivRem(i, 2, out rem);
            if (rem == 0)
            {
              nwItem.BackColor = System.Drawing.ColorTranslator.FromHtml("230,230,255");
            }
            this.resListView.Items.Add(nwItem);
          }
        }
        this.obeyChnge = true;
      }
      catch (Exception ex)
      {
        MessageBox.Show(ex.Message, "Rhomicom Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        return;
      }
    }

    private void resListView_ItemChecked(object sender, System.Windows.Forms.ItemCheckedEventArgs e)
    {
      if (e.Item.Equals(null) || this.obeyChnge == false)
      {
        return;
      }
      for (int i = 0; i < this.resListView.CheckedItems.Count; i++)
      {
        if (!(this.selAddrsTextBox.Text.Contains(this.resListView.CheckedItems[i].SubItems[2].Text)))
        {
          this.selAddrsTextBox.AppendText(this.resListView.CheckedItems[i].SubItems[2].Text + ";");
        }
        if (!(this.selNamesTextBox.Text.Contains(this.resListView.CheckedItems[i].SubItems[1].Text)))
        {
          this.selNamesTextBox.AppendText(this.resListView.CheckedItems[i].SubItems[1].Text + ";");
        }
      }
    }

    private void okButton_Click(object sender, EventArgs e)
    {
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