using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonCode
{
 public partial class calendarDiag : Form
 {
  public calendarDiag()
  {
   InitializeComponent();
  }
  bool obeyevnt = false;
  public string checkNFormatDate(string inStr)
  {
    DateTime dte1 = DateTime.Now;
    bool sccs = DateTime.TryParse(inStr, out dte1);
    if (!sccs)
    {
      dte1 = DateTime.Now;
    }
    return dte1.ToString("dd-MMM-yyyy HH:mm:ss");
  }

  private void calendarDiag_Load(object sender, EventArgs e)
  {
   obeyevnt = false;
   this.monthCalendar1.MaxSelectionCount = 1;
   if (this.selectedDateComboBox.Text == "")
   {
    this.selectedDateComboBox.Text = DateTime.Now.ToString("dd-MMM-yyyy HH:mm:ss");
   }
   obeyevnt = true;
  }

  private void gotoButton_Click(object sender, EventArgs e)
  {
   this.setDate();
  }

  private void OKButton_Click(object sender, EventArgs e)
  {
   DateTime tst_dte;
   if (this.selectedDateComboBox.Text == "")
   {
    this.selectedDateComboBox.Text = "";
   }
   else
   {
    if (DateTime.TryParse(this.selectedDateComboBox.Text, out tst_dte) == false)
    {
     MessageBox.Show("The Date typed is Invalid!", "Rhomicom Message!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
     return;
    }
    else
    {
     this.selectedDateComboBox.Text = tst_dte.ToString("dd-MMM-yyyy HH:mm:ss");
    }
   }
   this.DialogResult = DialogResult.OK;
   this.Close();
  }

  public void setDate()
  {
   DateTime tst_dte;
   if (DateTime.TryParse(this.selectedDateComboBox.Text, out tst_dte) == true)
   {
    this.monthCalendar1.SelectionStart = tst_dte;
   }
   if (obeyevnt == false)
   {
     return;
   }
   //this.OKButton.PerformClick();
  }

  private void monthCalendar1_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
  {
   if (obeyevnt == false)
   {
    return;
   }
   if (e != null)
   {
     this.obeyevnt = false;
    this.selectedDateComboBox.Text = e.Start.ToString("dd-MMM-yyyy HH:mm:ss");
    this.obeyevnt = true;
   }
   System.Windows.Forms.Application.DoEvents();
   this.OKButton.PerformClick();
  }

  private void cancelButton_Click(object sender, EventArgs e)
  {
   this.DialogResult = DialogResult.Cancel;
   this.Close();
  }

  private void selectedDateComboBox_TextChanged(object sender, System.EventArgs e)
  {
   
  }

  private void fdButton_Click(object sender, EventArgs e)
  {
   this.selectedDateComboBox.Text = "31-Dec-4000 23:59:59";
   this.setDate();
  }

  private void selectedDateComboBox_Leave(object sender, EventArgs e)
  {
    if (obeyevnt == false)
    {
      return;
    }
    this.selectedDateComboBox.Text = this.checkNFormatDate(this.selectedDateComboBox.Text);    
    this.gotoButton_Click(this.gotoButton, e);
  }

  private void selectedDateComboBox_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
    {
      this.OKButton.PerformClick();
    }
  }

 }
}