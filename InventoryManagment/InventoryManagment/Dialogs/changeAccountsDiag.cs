using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StoresAndInventoryManager.Classes;

namespace StoresAndInventoryManager.Dialogs
{
    public partial class changeAccountsDiag : Form
    {
        public changeAccountsDiag()
        {
            InitializeComponent();
        }
        string srchWrd = "%";
        public string slctdAcntIDs = "";
        public bool editMode = false;
        private void cogsbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string[] selVals = new string[1];
                selVals[0] = this.cogsIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.cogsIDtextBox.Text = selVals[i];
                        this.cogsAcctextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void salesRevbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string[] selVals = new string[1];
                selVals[0] = this.salesRevIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Revenue Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {

                    for (int i = 0; i < selVals.Length; i++)
                    {
                        if (Global.mnFrm.cmCde.isAccntContra(int.Parse(selVals[i])) == "1")
                        {
                            Global.mnFrm.cmCde.showMsg("Cannot Put a Contra Account Here!", 0);
                            this.salesRevIDtextBox.Text = "-1";
                            this.salesRevtextBox.Text = "";
                            return;
                        }
                        this.salesRevIDtextBox.Text = selVals[i];
                        this.salesRevtextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void salesRetbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string[] selVals = new string[1];
                selVals[0] = this.salesRetIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Contra Revenue Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.salesRetIDtextBox.Text = selVals[i];
                        this.salesRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void purcRetbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string[] selVals = new string[1];
                selVals[0] = this.purcRetIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Contra Expense Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        this.purcRetIDtextBox.Text = selVals[i];
                        this.purcRettextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void expnsbutton_Click(object sender, EventArgs e)
        {
            try
            {
                string[] selVals = new string[1];
                selVals[0] = this.expnsIDtextBox.Text;
                DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
                    Global.mnFrm.cmCde.getLovID("Expense Accounts"), ref selVals,
                    true, false, Global.mnFrm.cmCde.Org_id,
               this.srchWrd, "Both", false);
                if (dgRes == DialogResult.OK)
                {
                    for (int i = 0; i < selVals.Length; i++)
                    {
                        if (Global.mnFrm.cmCde.isAccntContra(int.Parse(selVals[i])) == "1")
                        {
                            Global.mnFrm.cmCde.showMsg("Cannot Put a Contra Account Here!", 0);
                            this.expnsIDtextBox.Text = "-1";
                            this.expnstextBox.Text = "";
                            return;
                        }
                        this.expnsIDtextBox.Text = selVals[i];
                        this.expnstextBox.Text = Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
                    }
                }
            }
            catch (Exception ex)
            {
                Global.mnFrm.cmCde.showMsg(ex.Message, 0);
                return;
            }
        }

        private void changeAccountsDiag_Load(object sender, EventArgs e)
        {
            Color[] clrs = Global.mnFrm.cmCde.getColors();
            this.BackColor = clrs[0];
            char[] w = { ',' };
            string[] inbrghtIDs = slctdAcntIDs.Split(w);
            for (int i = 0; i < inbrghtIDs.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        this.cogsIDtextBox.Text = inbrghtIDs[i];
                        this.cogsAcctextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(inbrghtIDs[i])) + "." +
                            Global.mnFrm.cmCde.getAccntName(int.Parse(inbrghtIDs[i]));
                        break;
                    case 1:
                        this.salesRevIDtextBox.Text = inbrghtIDs[i];
                        this.salesRevtextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(inbrghtIDs[i])) + "." +
                            Global.mnFrm.cmCde.getAccntName(int.Parse(inbrghtIDs[i]));
                        break;
                    case 2:
                        this.salesRetIDtextBox.Text = inbrghtIDs[i];
                        this.salesRettextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(inbrghtIDs[i])) + "." +
                            Global.mnFrm.cmCde.getAccntName(int.Parse(inbrghtIDs[i]));
                        break;
                    case 3:
                        this.purcRetIDtextBox.Text = inbrghtIDs[i];
                        this.purcRettextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(inbrghtIDs[i])) + "." +
                            Global.mnFrm.cmCde.getAccntName(int.Parse(inbrghtIDs[i]));
                        break;
                    case 4:
                        this.expnsIDtextBox.Text = inbrghtIDs[i];
                        this.expnstextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(inbrghtIDs[i])) + "." +
                            Global.mnFrm.cmCde.getAccntName(int.Parse(inbrghtIDs[i]));
                        break;

                }
            }
            this.cogsbutton.Enabled = this.editMode;
            this.salesRevbutton.Enabled = this.editMode;
            this.salesRetbutton.Enabled = this.editMode;
            this.purcRetbutton.Enabled = this.editMode;
            this.expnsbutton.Enabled = this.editMode;
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
