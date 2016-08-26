using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Enterprise_Management_System.Classes;

namespace Enterprise_Management_System.Dialogs
	{
	public partial class inboxDiag : Form
		{
		public inboxDiag()
			{
			InitializeComponent();
			}

    private void inboxDiag_Load(object sender, EventArgs e)
    {
      System.Windows.Forms.Application.DoEvents();
      Color[] clrs = Global.myNwMainFrm.cmnCdMn.getColors();
      this.BackColor = clrs[0];
    }
		}
	}