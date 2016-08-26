using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CommonCode
	{
	public partial class recHstryDiag : Form
		{
		public recHstryDiag()
			{
			InitializeComponent();
			}

		private void okButton_Click(object sender, EventArgs e)
			{
			this.DialogResult = DialogResult.OK;
			this.Close();
			}

    private void recHstryDiag_Load(object sender, EventArgs e)
    {

    }
		}
	}