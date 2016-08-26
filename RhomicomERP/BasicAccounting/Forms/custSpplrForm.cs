using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Accounting.Classes;
using Accounting.Dialogs;
using cadmaFunctions;
using Microsoft.VisualBasic;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Accounting.Forms
{
  public partial class custSpplrForm : Form
  {
    #region "GLOBAL VARIABLES..."
    //Records;
    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    bool obey_evnts = false;
    public bool txtChngd = false;
    string srchWrd = "%";
    bool addRec = false;
    bool editRec = false;
    bool addDtRec = false;
    bool editDtRec = false;

    bool addRecsP = false;
    bool editRecsP = false;
    bool delRecsP = false;
    bool beenToCheckBx = false;

    #endregion

    #region "FORM EVENTS..."
    public custSpplrForm()
    {
      InitializeComponent();
    }
    public void disableFormButtons()
    {
      bool vwSQL = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[9]);
      bool rcHstry = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[10]);
      this.addRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]);
      this.editRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]);
      this.delRecsP = Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]);

      this.saveButton.Enabled = false;
      this.addCstmrButton.Enabled = this.addRecsP;
      this.addSpplrButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;

      this.saveDtButton.Enabled = false;
      this.addDtButton.Enabled = this.addRecsP;
      this.editDtButton.Enabled = this.editRecsP;
      this.delDtButton.Enabled = this.delRecsP;

    }
    #endregion

    #region "CUSTOMERS & SUPPLIERS..."
    public void loadPanel()
    {
      this.obey_evnts = false;
      if (this.searchInComboBox.SelectedIndex < 0)
      {
        this.searchInComboBox.SelectedIndex = 0;
      }
      if (this.searchForTextBox.Text.Contains("%") == false)
      {
        this.searchForTextBox.Text = "%" + this.searchForTextBox.Text.Replace(" ", "%") + "%";
      }
      if (this.searchForTextBox.Text == "%%")
      {
        this.searchForTextBox.Text = "%";
      }
      int dsply = 0;
      if (this.dsplySizeComboBox.Text == ""
        || int.TryParse(this.dsplySizeComboBox.Text, out dsply) == false)
      {
        this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      }
      this.is_last_rec = false;
      this.totl_rec = Global.mnFrm.cmCde.Big_Val;
      this.getPnlData();
      this.obey_evnts = true;
    }

    private void getPnlData()
    {
      this.updtTotals();
      this.populateListVw();
      this.updtNavLabels();
    }

    private void updtTotals()
    {
      Global.mnFrm.cmCde.navFuncts.FindNavigationIndices(
        long.Parse(this.dsplySizeComboBox.Text), this.totl_rec);
      if (this.rec_cur_indx >= Global.mnFrm.cmCde.navFuncts.totalGroups)
      {
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      if (this.rec_cur_indx < 0)
      {
        this.rec_cur_indx = 0;
      }
      Global.mnFrm.cmCde.navFuncts.currentNavigationIndex = this.rec_cur_indx;
    }

    private void updtNavLabels()
    {
      this.moveFirstButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveFirstBtnStatus();
      this.movePreviousButton.Enabled = Global.mnFrm.cmCde.navFuncts.movePrevBtnStatus();
      this.moveNextButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveNextBtnStatus();
      this.moveLastButton.Enabled = Global.mnFrm.cmCde.navFuncts.moveLastBtnStatus();
      this.positionTextBox.Text = Global.mnFrm.cmCde.navFuncts.displayedRecordsNumbers();
      if (this.is_last_rec == true ||
        this.totl_rec != Global.mnFrm.cmCde.Big_Val)
      {
        this.totalRecsLabel.Text = Global.mnFrm.cmCde.navFuncts.totalRecordsLabel();
      }
      else
      {
        this.totalRecsLabel.Text = "of Total";
      }
    }

    private void populateListVw()
    {
      this.obey_evnts = false;
      DataSet dtst = Global.get_Basic_Cstmr(this.searchForTextBox.Text,
        this.searchInComboBox.Text, this.rec_cur_indx,
        int.Parse(this.dsplySizeComboBox.Text), Global.mnFrm.cmCde.Org_id);
      this.cstSplrListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.last_rec_num = Global.mnFrm.cmCde.navFuncts.startIndex() + i;
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString(),
    dtst.Tables[0].Rows[i][2].ToString()});
        this.cstSplrListView.Items.Add(nwItem);
      }
      this.correctNavLbls(dtst);
      if (this.cstSplrListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.cstSplrListView.Items[0].Selected = true;
      }
      else
      {
        this.populateDet(-10000);
      }
      this.obey_evnts = true;
    }

    private void populateSitesListVw(int cstmrID)
    {
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_CstmrBscSites(cstmrID);
      this.siteListView.Items.Clear();

      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        ListViewItem nwItem = new ListViewItem(new string[] {
    (Global.mnFrm.cmCde.navFuncts.startIndex() + i).ToString(),
    dtst.Tables[0].Rows[i][1].ToString(),
    dtst.Tables[0].Rows[i][0].ToString()});
        this.siteListView.Items.Add(nwItem);
      }
      if (this.siteListView.Items.Count > 0)
      {
        this.obey_evnts = true;
        this.siteListView.Items[0].Selected = true;
      }
      else
      {
        this.populateSiteDet(-10000);
      }
      this.obey_evnts = true;
    }

    private void populateSiteDet(int cstmrSiteID)
    {
      this.clearSiteDetInfo();
      if (this.editDtRec == false)
      {
        this.disableSiteDetEdit();
      }
      this.obey_evnts = false;
      DataSet dtst = Global.get_One_CstmrSitesDt(cstmrSiteID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.siteIDTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();
        this.siteNmTextBox.Text = dtst.Tables[0].Rows[i][1].ToString();
        this.siteDescTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();
        this.isSiteEnabledCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(
          dtst.Tables[0].Rows[i][20].ToString());
        this.bnkNmTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();
        this.brnchNmTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();
        this.acntNumTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
        this.swiftCodeTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
        this.ibanNumTxtBox.Text = dtst.Tables[0].Rows[i][21].ToString();
        this.accCurIDTextBox.Text = dtst.Tables[0].Rows[i][22].ToString();
        this.accCurTextBox.Text = dtst.Tables[0].Rows[i][23].ToString();
        
        this.wthldngTaxTexBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id",
          "code_name", long.Parse(dtst.Tables[0].Rows[i][6].ToString()));
        this.wthTaxIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
        this.dscntTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes", "code_id",
          "code_name", long.Parse(dtst.Tables[0].Rows[i][7].ToString()));
        this.dscntIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        this.bllngAddrsTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
        this.shipAddrsTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
        this.cntctPrsnTextBox.Text = dtst.Tables[0].Rows[i][10].ToString();
        this.cntctNosTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();

        this.emailTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();

        this.ntnltyTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
        this.idTypeTextBox.Text = dtst.Tables[0].Rows[i][15].ToString();
        this.idNumTextBox.Text = dtst.Tables[0].Rows[i][16].ToString();
        this.dateIssuedTextBox.Text = dtst.Tables[0].Rows[i][17].ToString();
        this.expryDateTextBox.Text = dtst.Tables[0].Rows[i][18].ToString();
        this.otherInfoTextBox.Text = dtst.Tables[0].Rows[i][19].ToString();

      }
      this.obey_evnts = true;
    }

    private void populateDet(int cstmrID)
    {
      this.clearDetInfo();
      if (this.editRec == false)
      {
        this.disableDetEdit();
      }
      this.obey_evnts = false;
      Global.updtDOBs();
      DataSet dtst = Global.get_One_CstmrDet(cstmrID);
      for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
      {
        this.idTextBox.Text = dtst.Tables[0].Rows[i][0].ToString();//this.taxListView.SelectedItems[0].SubItems[2].Text;
        this.nameTextBox.Text = dtst.Tables[0].Rows[i][2].ToString();//this.taxListView.SelectedItems[0].SubItems[1].Text;
        this.descTextBox.Text = dtst.Tables[0].Rows[i][3].ToString();//this.taxListView.SelectedItems[0].SubItems[3].Text;
        if (this.editRec == false && this.addRec == false)
        {
          this.typeComboBox.Items.Clear();
          this.typeComboBox.Items.Add(dtst.Tables[0].Rows[i][1].ToString());
        }
        this.typeComboBox.SelectedItem = dtst.Tables[0].Rows[i][1].ToString();//;

        this.classfctnTextBox.Text = dtst.Tables[0].Rows[i][4].ToString();//this.taxListView.SelectedItems[0].SubItems[11].Text;
        this.lbltyAcntIDTextBox.Text = dtst.Tables[0].Rows[i][5].ToString();
        this.lbltyAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][5].ToString()))
          + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][5].ToString()));

        this.rcvblAccntIDTextBox.Text = dtst.Tables[0].Rows[i][6].ToString();
        this.rcvblAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[i][6].ToString()))
          + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(dtst.Tables[0].Rows[i][6].ToString()));
        this.lnkdPrsnIDTextBox.Text = dtst.Tables[0].Rows[i][7].ToString();
        this.lnkdPersonNoTextBox.Text =
          (Global.mnFrm.cmCde.getPrsnName(long.Parse(dtst.Tables[0].Rows[i][7].ToString()))
          + " (" + Global.mnFrm.cmCde.getPrsnLocID(long.Parse(dtst.Tables[0].Rows[i][7].ToString())) + ")").Replace(" ()", "");
        this.genderTextBox.Text = dtst.Tables[0].Rows[i][8].ToString();
        this.dobTextBox.Text = dtst.Tables[0].Rows[i][9].ToString();
        this.isEnabledCheckBox.Checked = Global.mnFrm.cmCde.cnvrtBitStrToBool(dtst.Tables[0].Rows[i][10].ToString());

        this.brndNmTextBox.Text = dtst.Tables[0].Rows[i][11].ToString();
        this.orgTypeTextBox.Text = dtst.Tables[0].Rows[i][12].ToString();
        this.regNumTextBox.Text = dtst.Tables[0].Rows[i][13].ToString();
        this.dateIncprtdTextBox.Text = dtst.Tables[0].Rows[i][14].ToString();
        this.typeOfIncpTextBox.Text = dtst.Tables[0].Rows[i][15].ToString();
        this.vatNumTextBox.Text = dtst.Tables[0].Rows[i][16].ToString();
        this.tinNumTextBox.Text = dtst.Tables[0].Rows[i][17].ToString();
        this.ssnitRegNumTextBox.Text = dtst.Tables[0].Rows[i][18].ToString();
        this.emplyeesNumUpDown.Value = decimal.Parse(dtst.Tables[0].Rows[i][19].ToString());
        this.servcOffrdTextBox.Text = dtst.Tables[0].Rows[i][20].ToString();

        this.srvcsListView.Items.Clear();
        char[] mychr = { '|' };
        string[] srvcsOffrd = dtst.Tables[0].Rows[i][21].ToString().Split(mychr, StringSplitOptions.RemoveEmptyEntries);
        for (int a = 0; a < srvcsOffrd.Length; a++)
        {
          if (srvcsOffrd[a] != "")
          {
            ListViewItem nwItem = new ListViewItem(new string[]{
				(a+1).ToString(),
				srvcsOffrd[a]});
            this.srvcsListView.Items.Add(nwItem);
          }
        }
      }
      this.populateSitesListVw(cstmrID);
      this.obey_evnts = true;
    }

    private string getSrvcsOffrd()
    {
      string srvcs = "";
      for (int i = 0; i < this.srvcsListView.Items.Count; i++)
      {
        srvcs += this.srvcsListView.Items[i].SubItems[1].Text;
        if (i < this.srvcsListView.Items.Count - 1)
        {
          srvcs += "|";
        }
      }
      return srvcs;
    }

    private void correctNavLbls(DataSet dtst)
    {
      long totlRecs = dtst.Tables[0].Rows.Count;
      if (this.rec_cur_indx == 0 && totlRecs == 0)
      {
        this.is_last_rec = true;
        this.totl_rec = 0;
        this.last_rec_num = 0;
        this.rec_cur_indx = 0;
        this.updtTotals();
        this.updtNavLabels();
      }
      else if (this.totl_rec == Global.mnFrm.cmCde.Big_Val
     && totlRecs < long.Parse(this.dsplySizeComboBox.Text))
      {
        this.totl_rec = this.last_rec_num;
        if (totlRecs == 0)
        {
          this.rec_cur_indx -= 1;
          this.updtTotals();
          this.populateListVw();
        }
        else
        {
          this.updtTotals();
        }
      }
    }

    private bool shdObeyEvts()
    {
      return this.obey_evnts;
    }

    private void PnlNavButtons(object sender, System.EventArgs e)
    {
      System.Windows.Forms.ToolStripButton sentObj = (System.Windows.Forms.ToolStripButton)sender;
      this.totalRecsLabel.Text = "";
      if (sentObj.Name.ToLower().Contains("first"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx = 0;
      }
      else if (sentObj.Name.ToLower().Contains("previous"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx -= 1;
      }
      else if (sentObj.Name.ToLower().Contains("next"))
      {
        this.is_last_rec = false;
        this.rec_cur_indx += 1;
      }
      else if (sentObj.Name.ToLower().Contains("last"))
      {
        this.is_last_rec = true;
        this.totl_rec = Global.get_Total_Cstmr(this.searchForTextBox.Text,
          this.searchInComboBox.Text, Global.mnFrm.cmCde.Org_id);
        this.updtTotals();
        this.rec_cur_indx = Global.mnFrm.cmCde.navFuncts.totalGroups - 1;
      }
      this.getPnlData();
    }

    private void clearDetInfo()
    {
      this.obey_evnts = false;
      //this.saveButton.Enabled = false;
      this.addCstmrButton.Enabled = this.addRecsP;
      this.addSpplrButton.Enabled = this.addRecsP;
      this.editButton.Enabled = this.editRecsP;
      this.delButton.Enabled = this.delRecsP;
      this.idTextBox.Text = "-1";
      this.nameTextBox.Text = "";
      //this.typeComboBox.Items.Clear();
      this.descTextBox.Text = "";
      this.classfctnTextBox.Text = "";
      this.lbltyAcntIDTextBox.Text = "-1";
      this.lbltyAcntTextBox.Text = "";
      this.lnkdPrsnIDTextBox.Text = "-1";
      this.lnkdPersonNoTextBox.Text = "";
      this.isEnabledCheckBox.Checked = false;

      this.rcvblAccntIDTextBox.Text = "-1";
      this.rcvblAccntTextBox.Text = "";
      this.genderTextBox.Text = "Not Applicable";
      this.dobTextBox.Text = Global.mnFrm.cmCde.getFrmtdDB_Date_time().Substring(0, 11);

      this.brndNmTextBox.Text = "";
      this.orgTypeTextBox.Text = "";
      this.regNumTextBox.Text = "";
      this.dateIncprtdTextBox.Text = "";
      this.typeOfIncpTextBox.Text = "";
      this.vatNumTextBox.Text = "";
      this.tinNumTextBox.Text = "";
      this.ssnitRegNumTextBox.Text = "";
      this.emplyeesNumUpDown.Value = 0;
      this.servcOffrdTextBox.Text = "";
      this.srvcsListView.Items.Clear();
      this.obey_evnts = true;
    }

    private void prpareForDetEdit()
    {
      this.saveButton.Enabled = true;
      this.nameTextBox.ReadOnly = false;
      this.nameTextBox.BackColor = Color.FromArgb(255, 255, 128);
      this.descTextBox.ReadOnly = false;
      this.descTextBox.BackColor = Color.White;

      this.lnkdPersonNoTextBox.ReadOnly = false;
      this.lnkdPersonNoTextBox.BackColor = Color.White;

      this.classfctnTextBox.ReadOnly = false;
      this.classfctnTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.genderTextBox.ReadOnly = false;
      this.genderTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.dobTextBox.ReadOnly = false;
      this.dobTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.lbltyAcntTextBox.ReadOnly = false;
      this.lbltyAcntTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.rcvblAccntTextBox.ReadOnly = false;
      this.rcvblAccntTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.brndNmTextBox.ReadOnly = false;
      this.brndNmTextBox.BackColor = Color.White;

      this.orgTypeTextBox.ReadOnly = true;
      this.orgTypeTextBox.BackColor = Color.White;

      this.regNumTextBox.ReadOnly = false;
      this.regNumTextBox.BackColor = Color.White;

      this.dateIncprtdTextBox.ReadOnly = true;
      this.dateIncprtdTextBox.BackColor = Color.White;

      this.typeOfIncpTextBox.ReadOnly = true;
      this.typeOfIncpTextBox.BackColor = Color.White;

      this.vatNumTextBox.ReadOnly = false;
      this.vatNumTextBox.BackColor = Color.White;

      this.tinNumTextBox.ReadOnly = false;
      this.tinNumTextBox.BackColor = Color.White;

      this.ssnitRegNumTextBox.ReadOnly = false;
      this.ssnitRegNumTextBox.BackColor = Color.White;

      this.emplyeesNumUpDown.ReadOnly = false;
      this.emplyeesNumUpDown.Increment = 1;
      this.emplyeesNumUpDown.BackColor = Color.White;

      this.servcOffrdTextBox.ReadOnly = false;
      this.servcOffrdTextBox.BackColor = Color.White;

      string selItm = this.typeComboBox.Text;
      this.typeComboBox.Items.Clear();
      this.typeComboBox.Items.Add("Customer");
      this.typeComboBox.Items.Add("Supplier");
      this.typeComboBox.Items.Add("Customer/Supplier");
      if (this.editRec == true)
      {
        this.typeComboBox.SelectedItem = selItm;
      }

    }

    private void disableDetEdit()
    {
      this.addRec = false;
      this.editRec = false;
      this.saveButton.Enabled = false;
      this.editButton.Text = "EDIT";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.BackColor = Color.WhiteSmoke;
      this.descTextBox.ReadOnly = true;
      this.descTextBox.BackColor = Color.WhiteSmoke;
      this.classfctnTextBox.ReadOnly = true;
      this.classfctnTextBox.BackColor = Color.WhiteSmoke;

      this.genderTextBox.ReadOnly = true;
      this.genderTextBox.BackColor = Color.WhiteSmoke;

      this.dobTextBox.ReadOnly = true;
      this.dobTextBox.BackColor = Color.WhiteSmoke;

      this.lnkdPersonNoTextBox.ReadOnly = true;
      this.lnkdPersonNoTextBox.BackColor = Color.WhiteSmoke;

      this.lbltyAcntTextBox.ReadOnly = true;
      this.lbltyAcntTextBox.BackColor = Color.WhiteSmoke;

      this.rcvblAccntTextBox.ReadOnly = true;
      this.rcvblAccntTextBox.BackColor = Color.WhiteSmoke;

      this.brndNmTextBox.ReadOnly = true;
      this.brndNmTextBox.BackColor = Color.WhiteSmoke;

      this.orgTypeTextBox.ReadOnly = true;
      this.orgTypeTextBox.BackColor = Color.WhiteSmoke;

      this.regNumTextBox.ReadOnly = true;
      this.regNumTextBox.BackColor = Color.WhiteSmoke;

      this.dateIncprtdTextBox.ReadOnly = true;
      this.dateIncprtdTextBox.BackColor = Color.WhiteSmoke;

      this.typeOfIncpTextBox.ReadOnly = true;
      this.typeOfIncpTextBox.BackColor = Color.WhiteSmoke;

      this.vatNumTextBox.ReadOnly = true;
      this.vatNumTextBox.BackColor = Color.WhiteSmoke;

      this.tinNumTextBox.ReadOnly = true;
      this.tinNumTextBox.BackColor = Color.WhiteSmoke;

      this.ssnitRegNumTextBox.ReadOnly = true;
      this.ssnitRegNumTextBox.BackColor = Color.WhiteSmoke;

      this.emplyeesNumUpDown.ReadOnly = true;
      this.emplyeesNumUpDown.Increment = 0;
      this.emplyeesNumUpDown.BackColor = Color.WhiteSmoke;

      this.servcOffrdTextBox.ReadOnly = true;
      this.servcOffrdTextBox.BackColor = Color.WhiteSmoke;
    }

    private void clearSiteDetInfo()
    {
      this.obey_evnts = false;
      //this.saveDtButton.Enabled = false;
      this.addDtButton.Enabled = this.addRecsP;
      this.editDtButton.Enabled = this.editRecsP;
      this.delDtButton.Enabled = this.delRecsP;
      this.siteIDTextBox.Text = "-1";
      this.siteNmTextBox.Text = "";
      this.siteDescTextBox.Text = "";
      this.bnkNmTextBox.Text = "";
      this.brnchNmTextBox.Text = "";
      this.acntNumTextBox.Text = "";
      this.wthldngTaxTexBox.Text = "";
      this.wthTaxIDTextBox.Text = "-1";
      this.dscntTextBox.Text = "";
      this.dscntIDTextBox.Text = "-1";
      this.bllngAddrsTextBox.Text = "";
      this.shipAddrsTextBox.Text = "";
      this.cntctPrsnTextBox.Text = "";
      this.cntctNosTextBox.Text = "";
      this.emailTextBox.Text = "";
      this.isSiteEnabledCheckBox.Checked = false;

      this.swiftCodeTextBox.Text = "";

      this.ibanNumTxtBox.Text = "";
      this.accCurTextBox.Text = "";
      this.accCurIDTextBox.Text = "-1";

      this.ntnltyTextBox.Text = "";
      this.idTypeTextBox.Text = "";
      this.idNumTextBox.Text = "";
      this.dateIssuedTextBox.Text = "";
      this.expryDateTextBox.Text = "";
      this.otherInfoTextBox.Text = "";
      this.obey_evnts = true;
    }

    private void prpareForSiteDetEdit()
    {
      this.saveDtButton.Enabled = true;
      this.siteNmTextBox.ReadOnly = false;
      this.siteNmTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.siteDescTextBox.ReadOnly = false;
      this.siteDescTextBox.BackColor = Color.White;

      this.swiftCodeTextBox.ReadOnly = false;
      this.swiftCodeTextBox.BackColor = Color.White;

      this.ibanNumTxtBox.ReadOnly = false;
      this.ibanNumTxtBox.BackColor = Color.White;

      this.accCurTextBox.ReadOnly = true;
      this.accCurTextBox.BackColor = Color.White;

      this.ntnltyTextBox.ReadOnly = false;
      this.ntnltyTextBox.BackColor = Color.White;

      this.idTypeTextBox.ReadOnly = false;
      this.idTypeTextBox.BackColor = Color.White;

      this.idNumTextBox.ReadOnly = false;
      this.idNumTextBox.BackColor = Color.White;

      this.dateIssuedTextBox.ReadOnly = false;
      this.dateIssuedTextBox.BackColor = Color.White;

      this.expryDateTextBox.ReadOnly = false;
      this.expryDateTextBox.BackColor = Color.White;

      this.otherInfoTextBox.ReadOnly = false;
      this.otherInfoTextBox.BackColor = Color.White;

      this.bnkNmTextBox.ReadOnly = false;
      this.bnkNmTextBox.BackColor = Color.White;

      this.brnchNmTextBox.ReadOnly = false;
      this.brnchNmTextBox.BackColor = Color.White;

      this.acntNumTextBox.ReadOnly = false;
      this.acntNumTextBox.BackColor = Color.White;

      this.bllngAddrsTextBox.ReadOnly = false;
      this.bllngAddrsTextBox.BackColor = Color.White;

      this.shipAddrsTextBox.ReadOnly = false;
      this.shipAddrsTextBox.BackColor = Color.White;

      this.cntctPrsnTextBox.ReadOnly = false;
      this.cntctPrsnTextBox.BackColor = Color.FromArgb(255, 255, 128);

      this.cntctNosTextBox.ReadOnly = false;
      this.cntctNosTextBox.BackColor = Color.White;

      this.emailTextBox.ReadOnly = false;
      this.emailTextBox.BackColor = Color.White;

      if (this.wthTaxButton.Enabled == true)
      {
        this.wthldngTaxTexBox.ReadOnly = false;
        this.wthldngTaxTexBox.BackColor = Color.White;
      }
      else
      {
        this.wthldngTaxTexBox.ReadOnly = true;
        this.wthldngTaxTexBox.BackColor = Color.WhiteSmoke;
      }
      if (this.dscntButton.Enabled == true)
      {
        this.dscntTextBox.ReadOnly = false;
        this.dscntTextBox.BackColor = Color.White;
      }
      else
      {
        this.dscntTextBox.ReadOnly = true;
        this.dscntTextBox.BackColor = Color.WhiteSmoke;
      }
    }

    private void disableSiteDetEdit()
    {
      this.addDtRec = false;
      this.editDtRec = false;
      this.saveDtButton.Enabled = false;
      this.editDtButton.Text = "EDIT";
      this.siteNmTextBox.ReadOnly = true;
      this.siteNmTextBox.BackColor = Color.WhiteSmoke;

      this.siteDescTextBox.ReadOnly = true;
      this.siteDescTextBox.BackColor = Color.WhiteSmoke;

      this.swiftCodeTextBox.ReadOnly = true;
      this.swiftCodeTextBox.BackColor = Color.WhiteSmoke;
      this.ibanNumTxtBox.ReadOnly = true;
      this.ibanNumTxtBox.BackColor = Color.WhiteSmoke;

      this.accCurTextBox.ReadOnly = true;
      this.accCurTextBox.BackColor = Color.WhiteSmoke;

      this.ntnltyTextBox.ReadOnly = true;
      this.ntnltyTextBox.BackColor = Color.WhiteSmoke;

      this.idTypeTextBox.ReadOnly = true;
      this.idTypeTextBox.BackColor = Color.WhiteSmoke;

      this.idNumTextBox.ReadOnly = true;
      this.idNumTextBox.BackColor = Color.WhiteSmoke;

      this.dateIssuedTextBox.ReadOnly = true;
      this.dateIssuedTextBox.BackColor = Color.WhiteSmoke;

      this.expryDateTextBox.ReadOnly = true;
      this.expryDateTextBox.BackColor = Color.WhiteSmoke;

      this.otherInfoTextBox.ReadOnly = true;
      this.otherInfoTextBox.BackColor = Color.WhiteSmoke;

      this.bnkNmTextBox.ReadOnly = true;
      this.bnkNmTextBox.BackColor = Color.WhiteSmoke;

      this.brnchNmTextBox.ReadOnly = true;
      this.brnchNmTextBox.BackColor = Color.WhiteSmoke;

      this.acntNumTextBox.ReadOnly = true;
      this.acntNumTextBox.BackColor = Color.WhiteSmoke;

      this.bllngAddrsTextBox.ReadOnly = true;
      this.bllngAddrsTextBox.BackColor = Color.WhiteSmoke;

      this.shipAddrsTextBox.ReadOnly = true;
      this.shipAddrsTextBox.BackColor = Color.WhiteSmoke;

      this.cntctPrsnTextBox.ReadOnly = true;
      this.cntctPrsnTextBox.BackColor = Color.WhiteSmoke;

      this.cntctNosTextBox.ReadOnly = true;
      this.cntctNosTextBox.BackColor = Color.WhiteSmoke;

      this.emailTextBox.ReadOnly = true;
      this.emailTextBox.BackColor = Color.WhiteSmoke;

      this.wthldngTaxTexBox.ReadOnly = true;
      this.wthldngTaxTexBox.BackColor = Color.WhiteSmoke;
      this.dscntTextBox.ReadOnly = true;
      this.dscntTextBox.BackColor = Color.WhiteSmoke;

    }
    #endregion

    private void editButton_Click(object sender, EventArgs e)
    {
      if (this.editButton.Text == "EDIT")
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.idTextBox.Text == "" || this.idTextBox.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addRec = false;
        this.editRec = true;
        this.prpareForDetEdit();
        //this.addButton.Enabled = false;
        this.editButton.Text = "STOP";
        if (this.editDtButton.Text == "EDIT" && this.siteListView.SelectedItems.Count > 0)
        {
          this.editDtButton.PerformClick();
        }
        else
        {
          this.addDtButton.PerformClick();
        }
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = false;
        this.editButton.Enabled = this.editRecsP;
        this.addCstmrButton.Enabled = this.addRecsP;
        this.addSpplrButton.Enabled = this.addRecsP;
        this.editButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableDetEdit();
        System.Windows.Forms.Application.DoEvents();
        if (this.editDtButton.Text == "STOP")
        {
          this.editDtButton.PerformClick();
        }

        this.loadPanel();
      }
    }

    private void addButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearDetInfo();
      this.clearSiteDetInfo();
      this.siteListView.Items.Clear();

      this.addRec = true;
      this.editRec = false;
      this.prpareForDetEdit();
      ToolStripButton myBtn = (ToolStripButton)sender;
      if (myBtn.Text.Contains("CUSTOMER"))
      {
        this.typeComboBox.SelectedItem = "Customer";
        this.lbltyAcntIDTextBox.Text = Global.get_DfltSalesLbltyAcnt(Global.mnFrm.cmCde.Org_id).ToString();
        this.lbltyAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(this.lbltyAcntIDTextBox.Text))
          + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(this.lbltyAcntIDTextBox.Text));

        this.rcvblAccntIDTextBox.Text = Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id).ToString();
        this.rcvblAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(this.rcvblAccntIDTextBox.Text))
          + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(this.rcvblAccntIDTextBox.Text));
      }
      else
      {
        this.typeComboBox.SelectedItem = "Supplier";
        this.lbltyAcntIDTextBox.Text = Global.get_DfltPyblAcnt(Global.mnFrm.cmCde.Org_id).ToString();
        this.lbltyAcntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(this.lbltyAcntIDTextBox.Text))
          + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(this.lbltyAcntIDTextBox.Text));

        this.rcvblAccntIDTextBox.Text = Global.get_DfltRcptRcvblAcnt(Global.mnFrm.cmCde.Org_id).ToString();
        this.rcvblAccntTextBox.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(this.rcvblAccntIDTextBox.Text))
          + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(this.rcvblAccntIDTextBox.Text));
      }
      this.addCstmrButton.Enabled = false;
      this.addSpplrButton.Enabled = false;
      this.editButton.Enabled = false;
      this.addDtButton.PerformClick();
      this.nameTextBox.Focus();
      this.nameTextBox.SelectAll();
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == true)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\r\nContact your System Administrator!", 0);
          return;
        }
        this.saveDtButton.Enabled = true;
      }
      else
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      if (!this.checkRqrmnts())
      {
        return;
      }
      if (this.addRec == true)
      {
        Global.createCstSplrRec(Global.mnFrm.cmCde.Org_id, this.nameTextBox.Text,
          this.descTextBox.Text, this.typeComboBox.Text, this.classfctnTextBox.Text,
          int.Parse(this.lbltyAcntIDTextBox.Text), int.Parse(this.rcvblAccntIDTextBox.Text),
          long.Parse(this.lnkdPrsnIDTextBox.Text), this.genderTextBox.Text, this.dobTextBox.Text,
          this.isEnabledCheckBox.Checked, this.brndNmTextBox.Text, this.orgTypeTextBox.Text,
          this.regNumTextBox.Text, this.dateIncprtdTextBox.Text, this.typeOfIncpTextBox.Text,
          this.vatNumTextBox.Text, this.tinNumTextBox.Text, this.ssnitRegNumTextBox.Text,
          (int)this.emplyeesNumUpDown.Value, this.servcOffrdTextBox.Text, this.getSrvcsOffrd());

        //this.saveButton.Enabled = false;
        this.addRec = false;
        this.editRec = true;
        this.editButton.Enabled = this.editRecsP;
        this.addCstmrButton.Enabled = this.addRecsP;
        this.addSpplrButton.Enabled = this.addRecsP;

        //Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        this.idTextBox.Text = Global.getCstmrSplrID(this.nameTextBox.Text, Global.mnFrm.cmCde.Org_id).ToString();
        System.Windows.Forms.Application.DoEvents();
        this.saveButton.Enabled = false;
        if (this.saveDtButton.Enabled == true)
        {
          this.saveDtButton.PerformClick();
        }
        else
        {
          Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        }

        ListViewItem nwItem = new ListViewItem(new string[] {
    "New",
    this.nameTextBox.Text,
    this.idTextBox.Text,
    this.typeComboBox.Text});

        this.cstSplrListView.Items.Insert(0, nwItem);
        while (this.cstSplrListView.SelectedItems.Count > 0)
        {
          this.cstSplrListView.SelectedItems[0].Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
          this.cstSplrListView.SelectedItems[0].Selected = false;
        };

        this.cstSplrListView.Items[0].Selected = true;
        this.editButton.PerformClick();
        //this.addDtButton.PerformClick();

        //this.saveButton.Enabled = true;
        System.Windows.Forms.Application.DoEvents();
        //this.loadPanel();
        this.saveButton.Enabled = true;
      }
      else if (this.editRec == true)
      {
        Global.updtCstSplrRec(int.Parse(this.idTextBox.Text), this.nameTextBox.Text,
          this.descTextBox.Text, this.typeComboBox.Text, this.classfctnTextBox.Text,
          int.Parse(this.lbltyAcntIDTextBox.Text), int.Parse(this.rcvblAccntIDTextBox.Text),
          long.Parse(this.lnkdPrsnIDTextBox.Text), this.genderTextBox.Text, this.dobTextBox.Text,
          this.isEnabledCheckBox.Checked, this.brndNmTextBox.Text, this.orgTypeTextBox.Text,
          this.regNumTextBox.Text, this.dateIncprtdTextBox.Text, this.typeOfIncpTextBox.Text,
          this.vatNumTextBox.Text, this.tinNumTextBox.Text, this.ssnitRegNumTextBox.Text,
          (int)this.emplyeesNumUpDown.Value, this.servcOffrdTextBox.Text, this.getSrvcsOffrd());

        if (this.cstSplrListView.SelectedItems.Count > 0)
        {
          this.cstSplrListView.SelectedItems[0].SubItems[1].Text = this.nameTextBox.Text;
        }
        this.saveButton.Enabled = false;
        if (this.saveDtButton.Enabled == true)
        {
          this.saveDtButton.PerformClick();
        }
        else
        {
          Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        }
        this.saveButton.Enabled = true;
      }
    }

    private bool checkRqrmnts()
    {
      if (this.nameTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Customer/Supplier Name!", 0);
        return false;
      }
      if (this.lbltyAcntIDTextBox.Text == "" || this.lbltyAcntIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Customer/Supplier Liability Account!", 0);
        return false;
      }
      if (this.rcvblAccntIDTextBox.Text == "" || this.rcvblAccntIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Customer/Supplier Receivable Account!", 0);
        return false;
      }
      long oldRecID = Global.getCstmrSplrID(this.nameTextBox.Text,
          Global.mnFrm.cmCde.Org_id);
      if (oldRecID > 0
       && this.addRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Customer/Supplier Name is already in use in this Organisation!", 0);
        return false;
      }
      if (oldRecID > 0
       && this.editRec == true
       && oldRecID.ToString() !=
       this.idTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Customer/Supplier Name is already in use in this Organisation!", 0);
        return false;
      }

      if (this.typeComboBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Customer/Supplier Type cannot be empty!", 0);
        return false;
      }
      if (this.classfctnTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Customer/Supplier Classification cannot be empty!", 0);
        return false;
      }

      return true;
    }

    private bool checkDtRqrmnts(int cstmrID)
    {
      if (this.siteNmTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Please enter a Site Name!", 0);
        return false;
      }

      long oldRecID = Global.getCstmrSplrSiteID(this.siteNmTextBox.Text,
          cstmrID);
      if (oldRecID > 0
       && this.addDtRec == true)
      {
        Global.mnFrm.cmCde.showMsg("Site Name is already in use by this Customer/Supplier!", 0);
        return false;
      }
      if (oldRecID > 0
       && this.editDtRec == true
       && oldRecID.ToString() !=
       this.siteIDTextBox.Text)
      {
        Global.mnFrm.cmCde.showMsg("New Site Name is already in use by this Customer/Supplier!", 0);
        return false;
      }

      //if (this.bllngAddrsTextBox.Text == "")
      //{
      // Global.mnFrm.cmCde.showMsg("Billing Address cannot be empty!", 0);
      // return false;
      //}
      //if (this.shipAddrsTextBox.Text == "")
      //{
      // Global.mnFrm.cmCde.showMsg("Shipping Address cannot be empty!", 0);
      // return false;
      //}
      if (this.cntctPrsnTextBox.Text == "")
      {
        Global.mnFrm.cmCde.showMsg("Contact Person cannot be empty!", 0);
        return false;
      }
      //if (this.cntctNosTextBox.Text == "")
      //{
      //  Global.mnFrm.cmCde.showMsg("Contact Numbers cannot be empty!", 0);
      //  return false;
      //}
      return true;
    }

    private void delButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.idTextBox.Text == "" || this.idTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (Global.isCstSplrInUse(int.Parse(this.idTextBox.Text)) == true)
      {
        Global.mnFrm.cmCde.showMsg("This Record is in Use!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.idTextBox.Text),
        "Customer/Supplier Name=" + this.nameTextBox.Text, "scm.scm_cstmr_suplr", "cust_sup_id");
      Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.idTextBox.Text),
        "Customer/Supplier Name=" + this.nameTextBox.Text, "scm.scm_cstmr_suplr_sites", "cust_supplier_id");
      this.loadPanel();
    }

    private void vwSQLButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.rec_SQL, 9);
    }

    private void rcHstryButton_Click(object sender, EventArgs e)
    {
      if (this.cstSplrListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.cstSplrListView.SelectedItems[0].SubItems[2].Text),
        "scm.scm_cstmr_suplr", "cust_sup_id"), 10);
    }

    private void goButton_Click(object sender, EventArgs e)
    {
      this.loadPanel();
    }

    private void searchForTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
      {
        this.goButton_Click(this.rfrshButton, ex);
      }
    }

    private void positionTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();
      if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Up)
      {
        this.PnlNavButtons(this.movePreviousButton, ex);
      }
      else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Down)
      {
        this.PnlNavButtons(this.moveNextButton, ex);
      }
    }

    private void custSpplrForm_Load(object sender, EventArgs e)
    {
      Color[] clrs = Global.mnFrm.cmCde.getColors();
      this.BackColor = clrs[0];
      this.glsLabel3.TopFill = clrs[0];
      this.glsLabel3.BottomFill = clrs[1];
      this.tabPage1.BackColor = clrs[0];
      this.tabPage2.BackColor = clrs[0];
    }

    private void cstSplrListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
      }
    }

    private void cstSplrListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.cstSplrListView.SelectedItems.Count > 0)
      {
        this.populateDet(int.Parse(this.cstSplrListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateDet(-100000);
      }
    }

    private void go1Button_Click(object sender, EventArgs e)
    {
      this.rec_cur_indx = 0;
      this.loadPanel();
    }

    private void addSiteButton_Click(object sender, EventArgs e)
    {
      //if (this.idTextBox.Text == "" || this.idTextBox.Text == "-1")
      //{
      // Global.mnFrm.cmCde.showMsg("Please select a saved Customer/Supplier First!", 0);
      // return;
      //}
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }
      this.clearSiteDetInfo();
      this.addDtRec = true;
      this.editDtRec = false;
      this.prpareForSiteDetEdit();
      this.cntctPrsnTextBox.Text = this.nameTextBox.Text;
      long prsnID = long.Parse(this.lnkdPrsnIDTextBox.Text);
      if (prsnID > 0)
      {
        this.cntctNosTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_names_nos",
    "person_id", "cntct_no_mobl", prsnID);//
        this.emailTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_names_nos",
    "person_id", "email", prsnID);//email  res_address
        this.siteNmTextBox.Text = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
        this.siteDescTextBox.Text = this.siteNmTextBox.Text;
        this.bllngAddrsTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_names_nos",
    "person_id", "pstl_addrs", prsnID);
        this.shipAddrsTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_names_nos",
"person_id", "res_address", prsnID);
        this.ntnltyTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_names_nos",
"person_id", "nationality", prsnID);

      }
      this.addDtButton.Enabled = false;
      this.editDtButton.Enabled = false;
    }

    private void editSiteButton_Click(object sender, EventArgs e)
    {
      if (this.editDtButton.Text == "EDIT")
      {
        if (this.editButton.Text == "EDIT" && this.cstSplrListView.SelectedItems.Count > 0)
        {
          this.editButton.PerformClick();
        }

        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
              " this action!\nContact your System Administrator!", 0);
          return;
        }
        if (this.siteIDTextBox.Text == "" || this.siteIDTextBox.Text == "-1")
        {
          Global.mnFrm.cmCde.showMsg("No record to Edit!", 0);
          return;
        }
        this.addDtRec = false;
        this.editDtRec = true;
        this.prpareForSiteDetEdit();
        this.addDtButton.Enabled = false;
        this.editDtButton.Text = "STOP";
        //this.editMenuItem.Text = "STOP EDITING";
      }
      else
      {
        if (this.editButton.Text == "STOP")
        {
          this.editButton.PerformClick();
        }

        this.saveDtButton.Enabled = false;
        this.addDtRec = false;
        this.editDtRec = false;
        this.editDtButton.Enabled = this.addRecsP;
        this.addDtButton.Enabled = this.editRecsP;
        this.editDtButton.Text = "EDIT";
        //this.editMenuItem.Text = "Edit Item";
        this.disableSiteDetEdit();
        System.Windows.Forms.Application.DoEvents();

        this.populateSitesListVw(int.Parse(this.idTextBox.Text));
      }
    }

    private void saveSiteButton_Click(object sender, EventArgs e)
    {
      if (this.saveButton.Enabled == true)
      {
        this.saveDtButton.Enabled = false;
        this.saveButton.PerformClick();
        this.saveDtButton.Enabled = true;
      }
      if (this.addDtRec == true)
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      else
      {
        if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
        {
          Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
           " this action!\nContact your System Administrator!", 0);
          return;
        }
      }
      if (!this.checkDtRqrmnts(int.Parse(this.idTextBox.Text)))
      {
        return;
      }
      if (this.addDtRec == true)
      {
        Global.createCstSplrSiteRec(int.Parse(this.idTextBox.Text), this.siteNmTextBox.Text,
          this.siteDescTextBox.Text, this.cntctPrsnTextBox.Text, this.cntctNosTextBox.Text,
          this.emailTextBox.Text, this.bnkNmTextBox.Text, this.brnchNmTextBox.Text,
          this.acntNumTextBox.Text, this.bllngAddrsTextBox.Text, this.shipAddrsTextBox.Text,
          int.Parse(this.wthTaxIDTextBox.Text), int.Parse(this.dscntIDTextBox.Text),
          this.swiftCodeTextBox.Text, this.ntnltyTextBox.Text, this.idTypeTextBox.Text,
          this.idNumTextBox.Text, this.dateIssuedTextBox.Text, this.expryDateTextBox.Text,
          this.otherInfoTextBox.Text, this.isSiteEnabledCheckBox.Checked, this.ibanNumTxtBox.Text,
          int.Parse(this.accCurIDTextBox.Text));

        this.saveDtButton.Enabled = false;
        this.addDtRec = false;
        this.editDtRec = false;
        this.editDtButton.Enabled = this.addRecsP;
        this.addDtButton.Enabled = this.editRecsP;

        Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
        System.Windows.Forms.Application.DoEvents();
        //this.populateSitesListVw(int.Parse(this.idTextBox.Text));
      }
      else if (this.editDtRec == true)
      {
        Global.updtCstSplrSiteRec(int.Parse(this.siteIDTextBox.Text), this.siteNmTextBox.Text,
          this.siteDescTextBox.Text, this.cntctPrsnTextBox.Text, this.cntctNosTextBox.Text,
          this.emailTextBox.Text, this.bnkNmTextBox.Text, this.brnchNmTextBox.Text,
          this.acntNumTextBox.Text, this.bllngAddrsTextBox.Text, this.shipAddrsTextBox.Text,
          int.Parse(this.wthTaxIDTextBox.Text), int.Parse(this.dscntIDTextBox.Text),
          this.swiftCodeTextBox.Text, this.ntnltyTextBox.Text, this.idTypeTextBox.Text,
          this.idNumTextBox.Text, this.dateIssuedTextBox.Text, this.expryDateTextBox.Text,
          this.otherInfoTextBox.Text, this.isSiteEnabledCheckBox.Checked, this.ibanNumTxtBox.Text,
          int.Parse(this.accCurIDTextBox.Text));

        if (this.siteListView.SelectedItems.Count > 0)
        {
          this.siteListView.SelectedItems[0].SubItems[1].Text = this.siteNmTextBox.Text;
        }
        Global.mnFrm.cmCde.showMsg("Record Saved!", 3);
      }
    }

    private void delSiteButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[16]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
         " this action!\nContact your System Administrator!", 0);
        return;
      }

      if (this.siteIDTextBox.Text == "" || this.siteIDTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select the Record to DELETE!", 0);
        return;
      }
      if (Global.isCstSplrSiteInUse(int.Parse(this.siteIDTextBox.Text)) == true)
      {
        Global.mnFrm.cmCde.showMsg("This Record is in Use!", 0);
        return;
      }
      if (Global.mnFrm.cmCde.showMsg("Are you sure you want to DELETE the selected Record?" +
   "\r\nThis action cannot be undone!", 1) == DialogResult.No)
      {
        Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      Global.mnFrm.cmCde.deleteGnrlRecs(long.Parse(this.siteIDTextBox.Text),
        "Customer/Supplier Site Name=" + this.siteNmTextBox.Text,
        "scm.scm_cstmr_suplr_sites", "cust_sup_site_id");
      this.populateSitesListVw(int.Parse(this.idTextBox.Text));
    }

    private void vwSQLSiteButton_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.showSQL(this.recDt_SQL, 9);
    }

    private void rcHstrySiteButton_Click(object sender, EventArgs e)
    {
      if (this.siteListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select a Record First!", 0);
        return;
      }
      Global.mnFrm.cmCde.showRecHstry(
        Global.mnFrm.cmCde.get_Gnrl_Rec_Hstry(long.Parse(
        this.siteListView.SelectedItems[0].SubItems[2].Text),
        "scm.scm_cstmr_suplr_sites", "cust_sup_site_id"), 10);
    }

    private void rfrshDtButton_Click(object sender, EventArgs e)
    {
      if (this.cstSplrListView.SelectedItems.Count > 0)
      {
        this.populateDet(int.Parse(this.cstSplrListView.SelectedItems[0].SubItems[2].Text));
        this.populateSitesListVw(int.Parse(this.cstSplrListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateDet(-100000);
        this.populateSitesListVw(-100000);
      }
    }

    private void siteListView_ItemSelectionChanged(object sender, System.Windows.Forms.ListViewItemSelectionChangedEventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (e.IsSelected)
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Bold);
      }
      else
      {
        e.Item.Font = new Font("Tahoma", 8.25f, FontStyle.Regular);
      }
    }

    private void siteListView_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false)
      {
        return;
      }
      if (this.siteListView.SelectedItems.Count > 0)
      {
        this.populateSiteDet(int.Parse(this.siteListView.SelectedItems[0].SubItems[2].Text));
      }
      else
      {
        this.populateSiteDet(-100000);
      }
    }

    private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      //if (this.shdObeyEvts() == false)
      //{
      //  return;
      //}
      //if (this.typeComboBox.Text == "Customer")
      //{
      //  this.wthTaxIDTextBox.Text = "-1";
      //  this.wthldngTaxTexBox.Text = "";
      //  this.wthTaxButton.Enabled = false;
      //  this.dscntButton.Enabled = true;
      //}
      //else
      //{
      //  this.dscntIDTextBox.Text = "-1";
      //  this.dscntTextBox.Text = "";
      //  this.dscntButton.Enabled = false;
      //  this.wthTaxButton.Enabled = true;

      //}
    }

    private void clssfctnButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "";
      if (this.typeComboBox.Text == "Customer")
      {
        lovNm = "Customer Classifications";
      }
      else if (this.typeComboBox.Text == "Supplier")
      {
        lovNm = "Supplier Classifications";
      }
      else
      {
        lovNm = "Customer/Supplier Classifications";
      }
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(
        this.classfctnTextBox.Text, Global.mnFrm.cmCde.getLovID(lovNm));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.classfctnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void bnkNmButton_Click(object sender, EventArgs e)
    {
      if (this.addDtRec == false && this.editDtRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "Banks";
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(
        this.bnkNmTextBox.Text, Global.mnFrm.cmCde.getLovID(lovNm));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.bnkNmTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void brnchNmButton_Click(object sender, EventArgs e)
    {
      if (this.addDtRec == false && this.editDtRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "Bank Branches";
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(
        this.brnchNmTextBox.Text, Global.mnFrm.cmCde.getLovID(lovNm));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.brnchNmTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void wthTaxButton_Click(object sender, EventArgs e)
    {
      if (this.addDtRec == false && this.editDtRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.wthTaxIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Tax Codes"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.wthTaxIDTextBox.Text = selVals[i];
          this.wthldngTaxTexBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes",
            "code_id", "code_name", long.Parse(selVals[i]));
        }
      }
    }

    private void dscntButton_Click(object sender, EventArgs e)
    {
      if (this.addDtRec == false && this.editDtRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string[] selVals = new string[1];
      selVals[0] = this.dscntIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID("Discount Codes"), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.dscntIDTextBox.Text = selVals[i];
          this.dscntTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("scm.scm_tax_codes",
            "code_id", "code_name", long.Parse(selVals[i]));
        }
      }
    }

    private void addMenuItem_Click(object sender, EventArgs e)
    {
      this.addButton_Click(this.addCstmrButton, e);
    }

    private void editMenuItem_Click(object sender, EventArgs e)
    {
      this.editButton_Click(this.editButton, e);
    }

    private void delMenuItem_Click(object sender, EventArgs e)
    {
      this.delButton_Click(this.delButton, e);
    }

    private void exptExMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.cstSplrListView);
    }

    private void rfrshMenuItem_Click(object sender, EventArgs e)
    {
      this.goButton_Click(this.rfrshButton, e);
    }

    private void vwSQLMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLButton_Click(this.vwSQLButton, e);
    }

    private void rcHstryMenuItem_Click(object sender, EventArgs e)
    {
      this.rcHstryButton_Click(this.rcHstryButton, e);
    }

    private void addDtMenuItem_Click(object sender, EventArgs e)
    {
      this.addSiteButton_Click(this.addDtButton, e);
    }

    private void editDtMenuItem_Click(object sender, EventArgs e)
    {
      this.editSiteButton_Click(this.editDtButton, e);
    }

    private void delDtMenuItem_Click(object sender, EventArgs e)
    {
      this.delSiteButton_Click(this.delDtButton, e);
    }

    private void exptExDtMenuItem_Click(object sender, EventArgs e)
    {
      Global.mnFrm.cmCde.exprtToExcel(this.siteListView);
    }

    private void rfrshDtMenuItem_Click(object sender, EventArgs e)
    {
      this.rfrshDtButton_Click(this.rfrshDtButton, e);
    }

    private void vwSQLDtMenuItem_Click(object sender, EventArgs e)
    {
      this.vwSQLSiteButton_Click(this.vwSQLDtButton, e);
    }

    private void rcHstryDtMenuItem_Click(object sender, EventArgs e)
    {
      this.rcHstrySiteButton_Click(this.rcHstryDtButton, e);
    }

    private void classfctnTextBox_TextChanged(object sender, EventArgs e)
    {
      if (!this.obey_evnts)
      {
        return;
      }
      this.txtChngd = true;

    }

    private void classfctnTextBox_Leave(object sender, EventArgs e)
    {
      if (this.txtChngd == false)
      {
        return;
      }
      this.txtChngd = false;
      TextBox mytxt = (TextBox)sender;
      this.obey_evnts = false;
      string srchWrd = mytxt.Text;
      if (!mytxt.Text.Contains("%"))
      {
        srchWrd = "%" + srchWrd.Replace(" ", "%") + "%";
      }

      if (mytxt.Name == "classfctnTextBox")
      {
        this.clsfctnLOVSearch(srchWrd);
      }
      else if (mytxt.Name == "lbltyAcntTextBox")
      {
        this.accntNmLOVSearch(mytxt, srchWrd, "Liability Accounts");
      }
      else if (mytxt.Name == "rcvblAccntTextBox")
      {
        this.accntNmLOVSearch(mytxt, srchWrd, "Asset Accounts");
      }
      else if (mytxt.Name == "bnkNmTextBox")
      {
        string lovNm = "Banks";
        string[] rslts = Global.mnFrm.cmCde.checkNGetLOVValue(srchWrd, "Both", Global.mnFrm.cmCde.getLovID(lovNm), -1, "", "", "");
        if (rslts[1] == "" && rslts[0] == "-1")
        {
          this.bnkNmTextBox.Text = "";
          this.bnkNmButton_Click(this.bnkNmButton, e);
        }
        else
        {
          this.bnkNmTextBox.Text = rslts[1];
        }
      }
      else if (mytxt.Name == "brnchNmTextBox")
      {
        string lovNm = "Bank Branches";
        string[] rslts = Global.mnFrm.cmCde.checkNGetLOVValue(srchWrd, "Both", Global.mnFrm.cmCde.getLovID(lovNm), -1, "", "", "");
        if (rslts[1] == "" && rslts[0] == "-1")
        {
          this.brnchNmTextBox.Text = "";
          this.brnchNmButton_Click(this.brnchNmButton, e);
        }
        else
        {
          this.brnchNmTextBox.Text = rslts[1];
        }
      }
      else if (mytxt.Name == "wthldngTaxTexBox")
      {
        string lovNm = "Tax Codes";
        string[] rslts = Global.mnFrm.cmCde.checkNGetLOVValue(srchWrd, "Both",
          Global.mnFrm.cmCde.getLovID(lovNm), Global.mnFrm.cmCde.Org_id, "", "", "");
        if (rslts[1] == "" && rslts[0] == "-1")
        {
          this.wthldngTaxTexBox.Text = "";
          this.wthTaxIDTextBox.Text = "-1";
          this.wthTaxButton_Click(this.wthTaxButton, e);
        }
        else
        {
          this.wthTaxIDTextBox.Text = rslts[0];
          this.wthldngTaxTexBox.Text = rslts[1];
        }
      }
      else if (mytxt.Name == "dscntTextBox")
      {
        string lovNm = "Discount Codes";
        string[] rslts = Global.mnFrm.cmCde.checkNGetLOVValue(srchWrd, "Both",
          Global.mnFrm.cmCde.getLovID(lovNm), Global.mnFrm.cmCde.Org_id, "", "", "");
        if (rslts[1] == "" && rslts[0] == "-1")
        {
          this.dscntTextBox.Text = "";
          this.dscntIDTextBox.Text = "-1";
          this.dscntButton_Click(this.dscntButton, e);
        }
        else
        {
          this.dscntIDTextBox.Text = rslts[0];
          this.dscntTextBox.Text = rslts[1];
        }
      }
      else if (mytxt.Name == "lnkdPersonNoTextBox")
      {
        string lovNm = "Unlinked Persons (Customers/Suppliers)";
        string[] rslts = Global.mnFrm.cmCde.checkNGetLOVValue(srchWrd, "Both",
          Global.mnFrm.cmCde.getLovID(lovNm), Global.mnFrm.cmCde.Org_id, "", "", "");
        if (rslts[1] == "" && rslts[0] == "-1")
        {
          this.lnkdPersonNoTextBox.Text = "";
          this.lnkdPrsnIDTextBox.Text = "-1";
          this.cntctPersonButton_Click(this.lnkdPersonButton, e);
        }
        else
        {
          this.lnkdPrsnIDTextBox.Text = Global.mnFrm.cmCde.getPrsnID(rslts[0]).ToString();
          this.lnkdPersonNoTextBox.Text = rslts[0] + " - " + rslts[1];
        }
      }
      else if (mytxt.Name == "genderTextBox")
      {
        this.genderLOVSrch(srchWrd, true);
      }
      else if (mytxt.Name == "ntnltyTextBox")
      {
        this.ntnltyLOVSrch(srchWrd, true);
      }
      else if (mytxt.Name == "idTypeTextBox")
      {
        this.idTypLOVSrch(srchWrd, true);
      }
      else if (mytxt.Name == "dobTextBox")
      {
        this.dobTextBox.Text = Global.mnFrm.cmCde.checkNFormatDate(this.dobTextBox.Text).Substring(0, 11);
        //this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
      }
      this.obey_evnts = true;
      this.txtChngd = false;
    }

    private void clsfctnLOVSearch(string srchWrd)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }

      if (this.classfctnTextBox.Text == "")
      {
        this.classfctnTextBox.Text = "";
        return;
      }

      string lovNm = "";
      if (this.typeComboBox.Text == "Customer")
      {
        lovNm = "Customer Classifications";
      }
      else if (this.typeComboBox.Text == "Supplier")
      {
        lovNm = "Supplier Classifications";
      }
      else
      {
        lovNm = "Customer/Supplier Classifications";
      }
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(
        this.classfctnTextBox.Text, Global.mnFrm.cmCde.getLovID(lovNm));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
          true, false, srchWrd, "Both", true);

      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.classfctnTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void accntNmLOVSearch(TextBox mtTxt, string srchWrd, string lovNm)
    {
      if (!mtTxt.Text.Contains("%"))
      {
        if (mtTxt.Name == "lbltyAcntTextBox")
        {
          this.lbltyAcntIDTextBox.Text = "-1";
        }
        else
        {
          this.rcvblAccntIDTextBox.Text = "-1";
        }
      }

      string[] selVals = new string[1];
      if (mtTxt.Name == "lbltyAcntTextBox")
      {
        selVals[0] = this.lbltyAcntIDTextBox.Text;
      }
      else
      {
        selVals[0] = this.rcvblAccntIDTextBox.Text;
      }

      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
        true, true, Global.mnFrm.cmCde.Org_id,
       srchWrd, "Both", true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          if (Global.mnFrm.cmCde.isAccntContra(int.Parse(selVals[i])) == "1")
          {
            Global.mnFrm.cmCde.showMsg("Cannot Put a Contra Account Here!", 0);
            if (mtTxt.Name == "lbltyAcntTextBox")
            {
              this.lbltyAcntIDTextBox.Text = "-1";
            }
            else
            {
              this.rcvblAccntIDTextBox.Text = "-1";
            }
            mtTxt.Text = "";
            return;
          }

          if (mtTxt.Name == "lbltyAcntTextBox")
          {
            this.lbltyAcntIDTextBox.Text = selVals[i];
          }
          else
          {
            this.rcvblAccntIDTextBox.Text = selVals[i];
          }
          mtTxt.Text = Global.mnFrm.cmCde.getAccntNum(int.Parse(selVals[i])) + "." + Global.mnFrm.cmCde.getAccntName(int.Parse(selVals[i]));
        }
      }
    }

    private void lbltyAcntButton_Click(object sender, EventArgs e)
    {
      this.accntNmLOVSearch(this.lbltyAcntTextBox, "%", "Liability Accounts");
    }

    private void rcvblAccntButton_Click(object sender, EventArgs e)
    {
      this.accntNmLOVSearch(this.rcvblAccntTextBox, "%", "Asset Accounts");
    }

    private void custSpplrForm_KeyDown(object sender, KeyEventArgs e)
    {
      EventArgs ex = new EventArgs();

      if (e.Control && e.KeyCode == Keys.S)
      {
        if (this.saveButton.Enabled == true)
        {
          this.saveButton_Click(this.saveButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.N)
      {
        if (this.addCstmrButton.Enabled == true)
        {
          this.addButton_Click(this.addCstmrButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.E)
      {
        if (this.editButton.Enabled == true)
        {
          this.editButton_Click(this.editButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.R)
      {
        this.resetTrnsButton.PerformClick();
      }
      else if ((e.Control && e.KeyCode == Keys.F) || e.KeyCode == Keys.F5)
      {
        if (this.rfrshButton.Enabled == true)
        {
          this.goButton_Click(this.rfrshButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else if (e.Control && e.KeyCode == Keys.Delete)
      {
        if (this.delButton.Enabled == true)
        {
          this.delButton_Click(this.delButton, ex);
        }
        e.Handled = true;
        e.SuppressKeyPress = true;
      }
      else
      {
        e.Handled = false;
        e.SuppressKeyPress = false;
        if (this.cstSplrListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.cstSplrListView, e);
        }
        else if (this.siteListView.Focused)
        {
          Global.mnFrm.cmCde.listViewKeyDown(this.siteListView, e);
        }
      }
    }

    private void searchForTextBox_Click(object sender, EventArgs e)
    {
      this.searchForTextBox.SelectAll();
    }

    private void cntctPersonButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "Unlinked Persons (Customers/Suppliers)";
      string[] selVals = new string[1];
      selVals[0] = this.lnkdPrsnIDTextBox.Text;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
          true, false, Global.mnFrm.cmCde.Org_id);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          long prsnID = Global.mnFrm.cmCde.getPrsnID(selVals[i]);
          this.lnkdPersonNoTextBox.Text = Global.mnFrm.cmCde.getPrsnSurNameFrst(prsnID)
          + " (" + Global.mnFrm.cmCde.getPrsnLocID(prsnID) + ")";
          this.nameTextBox.Text = this.lnkdPersonNoTextBox.Text;
          this.descTextBox.Text = this.lnkdPersonNoTextBox.Text;
          this.genderTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_names_nos",
            "person_id", "gender", prsnID);

          this.dobTextBox.Text = Global.mnFrm.cmCde.getGnrlRecNm("prs.prsn_names_nos",
  "person_id", "to_char(to_timestamp(date_of_birth,'YYYY-MM-DD'),'DD-Mon-YYYY')", prsnID);

          this.lnkdPrsnIDTextBox.Text = prsnID.ToString();
          if (this.siteListView.Items.Count <= 0)
          {
            this.addDtButton.PerformClick();
          }
        }
      }
    }

    private void resetTrnsButton_Click(object sender, EventArgs e)
    {
      this.searchInComboBox.SelectedIndex = 0;
      this.searchForTextBox.Text = "%";
      this.dsplySizeComboBox.Text = Global.mnFrm.cmCde.get_CurPlcy_Mx_Dsply_Recs().ToString();
      this.disableDetEdit();
      this.disableSiteDetEdit();
      this.rec_cur_indx = 0;
      this.goButton_Click(this.rfrshButton, e);
    }

    private void genderButton_Click(object sender, EventArgs e)
    {
      this.genderLOVSrch("%", false);
    }

    private void genderLOVSrch(string srchWrd, bool autoLoad)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Gender
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.genderTextBox.Text,
       Global.mnFrm.cmCde.getLovID("Gender"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("Gender"), ref selVals, true, true,
       srchWrd, "Both", autoLoad);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.genderTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }
    private void dobButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.dobTextBox);
      if (this.dobTextBox.Text.Length > 11)
      {
        this.dobTextBox.Text = this.dobTextBox.Text.Substring(0, 11);
        //this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
      }
    }

    private void ntnltyButton_Click(object sender, EventArgs e)
    {
      this.ntnltyLOVSrch("%", false);
    }

    private void ntnltyLOVSrch(string srchWrd, bool autoLoad)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //Nationalities
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.ntnltyTextBox.Text,
        Global.mnFrm.cmCde.getLovID("Countries"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Countries"), ref selVals, true, false, srchWrd, "Both", autoLoad);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.ntnltyTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void idTypLOVSrch(string srchWrd, bool autoLoad)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //National ID Types
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(this.idTypeTextBox.Text,
        Global.mnFrm.cmCde.getLovID("National ID Types"));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("National ID Types"), ref selVals,
        true, false, srchWrd, "Both", autoLoad);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.idTypeTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void idTypeButton_Click(object sender, EventArgs e)
    {
      this.idTypLOVSrch("%", false);
    }

    private void dteIssuedButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }

      Global.mnFrm.cmCde.selectDate(ref this.dateIssuedTextBox);
      if (this.dateIssuedTextBox.Text.Length > 11)
      {
        this.dateIssuedTextBox.Text = this.dateIssuedTextBox.Text.Substring(0, 11);
      }
    }

    private void expryDateButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }

      Global.mnFrm.cmCde.selectDate(ref this.expryDateTextBox);
      if (this.expryDateTextBox.Text.Length > 11)
      {
        this.expryDateTextBox.Text = this.expryDateTextBox.Text.Substring(0, 11);
      }
    }

    private void isEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false
    || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.isEnabledCheckBox.Checked = !this.isEnabledCheckBox.Checked;
      }
    }

    private void isSiteEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      if (this.shdObeyEvts() == false
    || beenToCheckBx == true)
      {
        beenToCheckBx = false;
        return;
      }
      beenToCheckBx = true;
      if (this.addRec == false && this.editRec == false)
      {
        this.isSiteEnabledCheckBox.Checked = !this.isSiteEnabledCheckBox.Checked;
      }
    }

    private long checkNCreateCstmr(long prsnID)
    {
      long cstmrID = -1;
      long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
"scm.scm_cstmr_suplr", "lnkd_prsn_id", "cust_sup_id",
prsnID), out cstmrID);
      if (cstmrID <= 0)
      {
        DataSet prsDtst = Global.get_PrsnCstmrDet(prsnID);
        if (prsDtst.Tables[0].Rows.Count > 0)
        {
          string fllnm = prsDtst.Tables[0].Rows[0][0].ToString();
          string gndr = prsDtst.Tables[0].Rows[0][1].ToString();

          string dob = prsDtst.Tables[0].Rows[0][2].ToString();

          string telNos = prsDtst.Tables[0].Rows[0][3].ToString();
          string eml = prsDtst.Tables[0].Rows[0][4].ToString();
          string siteNm = "OFFICE";// Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id);
          string bllng = prsDtst.Tables[0].Rows[0][5].ToString();
          string shpAdrs = prsDtst.Tables[0].Rows[0][6].ToString();

          string ntnlty = prsDtst.Tables[0].Rows[0][7].ToString();

          Global.createCstSplrRec(Global.mnFrm.cmCde.Org_id, fllnm, fllnm, "Customer", "Individual",
            Global.get_DfltSalesLbltyAcnt(Global.mnFrm.cmCde.Org_id),
            Global.get_DfltRcvblAcnt(Global.mnFrm.cmCde.Org_id), prsnID, gndr, dob, true, "",
            "", "", "", "", "", "", "", 0, "", "");
          long.TryParse(Global.mnFrm.cmCde.getGnrlRecNm(
"scm.scm_cstmr_suplr", "lnkd_prsn_id", "cust_sup_id",
prsnID), out cstmrID);
          if (cstmrID > 0)
          {
            Global.createCstSplrSiteRec(cstmrID, siteNm, siteNm, fllnm, telNos,
              eml, "", "", "", bllng, shpAdrs, -1,
              -1, "", ntnlty, "", "", "", "", "", true, "", -1);
          }
        }
      }
      return cstmrID;
    }

    private void importPrsnsButton_Click(object sender, EventArgs e)
    {
      //Person Types
      int[] selVals = new int[1];
      selVals[0] = -1;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("Person Types"),
        ref selVals, true, true,
       "%", "Both", true);
      if (dgRes == DialogResult.OK)
      {
        DataSet dtst = Global.getUnlinkedPrsns(Global.mnFrm.cmCde.getPssblValNm(selVals[0]));
        for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
        {
          this.checkNCreateCstmr(long.Parse(dtst.Tables[0].Rows[i][0].ToString()));
        }
        this.go1Button.PerformClick();
      }
    }

    private void orgTypeButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "Organisation Types";
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(
        this.orgTypeTextBox.Text, Global.mnFrm.cmCde.getLovID(lovNm));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.orgTypeTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void typeOfIncpButton_Click(object sender, EventArgs e)
    {
      if (this.addRec == false && this.editRec == false)
      {
        Global.mnFrm.cmCde.showMsg("Must be in ADD/EDIT mode First!", 0);
        return;
      }
      string lovNm = "Types of Incorporation";
      int[] selVals = new int[1];
      selVals[0] = Global.mnFrm.cmCde.getPssblValID(
        this.orgTypeTextBox.Text, Global.mnFrm.cmCde.getLovID(lovNm));
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
          Global.mnFrm.cmCde.getLovID(lovNm), ref selVals,
          true, false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.orgTypeTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

    private void dateIncprtdButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      Global.mnFrm.cmCde.selectDate(ref this.dateIncprtdTextBox);
      if (this.dateIncprtdTextBox.Text.Length > 11)
      {
        this.dateIncprtdTextBox.Text = this.dateIncprtdTextBox.Text.Substring(0, 11);
        //this.ageLabel.Text = Global.computePrsnAge(this.dobTextBox.Text);
      }
    }

    private void addItmStDtButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      //"Divisions/Groups"
      int[] selVals = new int[1];
      selVals[0] = -1;
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
        Global.mnFrm.cmCde.getLovID("List of Professional Services"), ref selVals, false,
        false);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          ListViewItem nwItem = new ListViewItem(new string[]{
				(this.srvcsListView.Items.Count+1).ToString(), 
					Global.mnFrm.cmCde.getPssblValNm(selVals[i])});
          this.srvcsListView.Items.Add(nwItem);
        }
      }
    }

    private void delItmStDtButton_Click(object sender, EventArgs e)
    {
      if (this.editRec == false && this.addRec == false)
      {
        Global.mnFrm.cmCde.showMsg("You must be in ADD/EDIT mode first!", 0);
        return;
      }
      if (this.srvcsListView.SelectedItems.Count <= 0)
      {
        Global.mnFrm.cmCde.showMsg("Please select the Item(s) to Delete", 0);
        return;
      }
      int cnt = this.srvcsListView.SelectedItems.Count;
      for (int i = 0; i < cnt; i++)
      {
        this.srvcsListView.SelectedItems[0].Remove();
      }
      for (int i = 0; i < this.srvcsListView.Items.Count; i++)
      {
        this.srvcsListView.Items[i].Text = (i + 1).ToString();
      }
    }

    private void cstmrExtraInfoButton_Click(object sender, EventArgs e)
    {
      if (this.idTextBox.Text == ""
               || this.idTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("No record to View!", 0);
        return;
      }
      DialogResult dgres = Global.mnFrm.cmCde.showRowsExtInfDiag(
        Global.mnFrm.cmCde.getMdlGrpID("Customers/Suppliers"),
          long.Parse(this.idTextBox.Text), "accb.accb_all_other_info_table",
          this.nameTextBox.Text, this.editRecsP, 10, 9,
          "accb.accb_all_other_info_table_dflt_row_id_seq");
      if (dgres == DialogResult.OK)
      {
      }
    }

    private void vwAttchmntsButton_Click(object sender, EventArgs e)
    {
      if (this.idTextBox.Text == "" ||
    this.idTextBox.Text == "-1")
      {
        Global.mnFrm.cmCde.showMsg("Please select a saved Firm First!", 0);
        return;
      }
      attchmntsDiag nwDiag = new attchmntsDiag();
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[15]) == false)
      {
        nwDiag.addButton.Enabled = false;
        nwDiag.addButton.Visible = false;
        nwDiag.editButton.Enabled = false;
        nwDiag.editButton.Visible = false;
        nwDiag.delButton.Enabled = false;
        nwDiag.delButton.Visible = false;
      }
      nwDiag.prmKeyID = long.Parse(this.idTextBox.Text);
      nwDiag.fldrNm = Global.mnFrm.cmCde.getFirmsImgsDrctry();
      nwDiag.fldrTyp = 14;
      nwDiag.attchCtgry = 5;
      DialogResult dgres = nwDiag.ShowDialog();
      if (dgres == DialogResult.OK)
      {
      }
    }

    private void exportfirmsButton_Click(object sender, EventArgs e)
    {
      string rspnse = Interaction.InputBox("How many Trading Partners/Firms will you like to Export?" +
      "\r\n1=No Trading Partners(Empty Template)" +
      "\r\n2=All Trading Partners" +
    "\r\n3-Infinity=Specify the exact number of Trading Partners to Export\r\n",
      "Rhomicom", "1", (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Width / 2) - 170,
      (Global.mnFrm.cmCde.myComputer.Screen.Bounds.Height / 2) - 100);
      if (rspnse.Equals(string.Empty) || rspnse.Equals(null))
      {
        //Global.mnFrm.cmCde.showMsg("Operation Cancelled!", 4);
        return;
      }
      int rsponse = 0;
      bool rsps = int.TryParse(rspnse, out rsponse);
      if (rsps == false)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-Infinity", 4);
        return;
      }
      if (rsponse < 1)
      {
        Global.mnFrm.cmCde.showMsg("Invalid Option! Expecting 1-Infinity", 4);
        return;
      }
      this.exprtTrnsTmp(rsponse);
    }

    private void exprtTrnsTmp(int exprtTyp)
    {
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
      Global.mnFrm.cmCde.nwWrkBk.Worksheets.Add(Type.Missing, Type.Missing, 1, Type.Missing);
      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).MergeCells = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Value2 = Global.mnFrm.cmCde.getOrgName(Global.mnFrm.cmCde.Org_id).ToUpper();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Bold = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).Font.Size = 13;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B2:C3", Type.Missing).WrapText = true;
      Global.mnFrm.cmCde.trgtSheets[0].Shapes.AddPicture(Global.mnFrm.cmCde.getOrgImgsDrctry() + @"\" + Global.mnFrm.cmCde.Org_id + ".png",
          Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, 1, 1, 50, 50);

      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Font.Bold = true;
      ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, 1]).Value2 = "No.";
      string[] hdngs ={"Partner/Firm Name**","Partner/Firm Description","Partner Type(Customer/Supplier)**",
                        "Classification**", "Liability Account No.**","Receivables Account No.**", 
                        "Establishment Date**","Site Name**","Billing Address","Shipping Address",
                        "Contact Person**","Tel Nos.","Email","Withholding Tax Name", 
                        "Country**","Bank Name","Branch Name","Account Number",
                        "Currency Code","SWIFT/BIC CODE","IBAN Number",
                        "Company Brand Name","Type of Organisation","Company Registration Number",
                        "Date of Incorporation","Type of Incorporation","VAT Number","TIN Number",
                        "SSNIT No.","No. of Employees","Description of Services","List of Services"};

      for (int a = 0; a < hdngs.Length; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 162, 192));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 255, 255));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Font.Bold = true;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[5, (a + 2)]).Value2 = hdngs[a].ToUpper();
      }

      DataSet dtst;
      if (exprtTyp == 2)
      {
        dtst = Global.get_One_CstmrDetNSites(-1);
      }
      else if (exprtTyp > 2)
      {
        dtst = Global.get_One_CstmrDetNSites(exprtTyp);
      }
      else
      {
        dtst = Global.get_One_CstmrDetNSites(0);
      }

      for (int a = 0; a < dtst.Tables[0].Rows.Count; a++)
      {
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 1]).Value2 = a + 1;
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 2]).Value2 = dtst.Tables[0].Rows[a][2].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 3]).Value2 = dtst.Tables[0].Rows[a][3].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 4]).Value2 = dtst.Tables[0].Rows[a][1].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 5]).Value2 = dtst.Tables[0].Rows[a][4].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 6]).Value2 = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[a][5].ToString()));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 7]).Value2 = Global.mnFrm.cmCde.getAccntNum(int.Parse(dtst.Tables[0].Rows[a][6].ToString()));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 8]).Value2 = dtst.Tables[0].Rows[a][9].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 9]).Value2 = dtst.Tables[0].Rows[a][23].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 10]).Value2 = dtst.Tables[0].Rows[a][30].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 11]).Value2 = dtst.Tables[0].Rows[a][31].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 12]).Value2 = dtst.Tables[0].Rows[a][32].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 13]).Value2 = dtst.Tables[0].Rows[a][33].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 14]).Value2 = dtst.Tables[0].Rows[a][34].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 15]).Value2 = Global.getTaxNm(int.Parse(dtst.Tables[0].Rows[a][28].ToString()));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 16]).Value2 = dtst.Tables[0].Rows[a][36].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 17]).Value2 = dtst.Tables[0].Rows[a][25].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 18]).Value2 = dtst.Tables[0].Rows[a][26].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 19]).Value2 = dtst.Tables[0].Rows[a][27].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 20]).Value2 = Global.mnFrm.cmCde.getPssblValNm(int.Parse(dtst.Tables[0].Rows[a][44].ToString()));
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 21]).Value2 = dtst.Tables[0].Rows[a][35].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 22]).Value2 = dtst.Tables[0].Rows[a][43].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 23]).Value2 = dtst.Tables[0].Rows[a][11].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 24]).Value2 = dtst.Tables[0].Rows[a][12].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 25]).Value2 = dtst.Tables[0].Rows[a][13].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 26]).Value2 = dtst.Tables[0].Rows[a][14].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 27]).Value2 = dtst.Tables[0].Rows[a][15].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 28]).Value2 = dtst.Tables[0].Rows[a][16].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 29]).Value2 = dtst.Tables[0].Rows[a][17].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 30]).Value2 = dtst.Tables[0].Rows[a][18].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 31]).Value2 = dtst.Tables[0].Rows[a][19].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 32]).Value2 = dtst.Tables[0].Rows[a][20].ToString();
        ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[(a + 6), 33]).Value2 = dtst.Tables[0].Rows[a][21].ToString();
      }

      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).ColumnWidth = 10;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("A1:A65535", Type.Missing).WrapText = true;
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:AH65535", Type.Missing).Columns.AutoFit();
      Global.mnFrm.cmCde.trgtSheets[0].get_Range("B1:AH65535", Type.Missing).Rows.AutoFit();
    }

    private void importfirmsButton_Click(object sender, EventArgs e)
    {
      if (Global.mnFrm.cmCde.test_prmssns(Global.dfltPrvldgs[14]) == false)
      {
        Global.mnFrm.cmCde.showMsg("You don't have permission to perform" +
            " this action!\nContact your System Administrator!", 0);
        return;
      }

      this.openFileDialog1.RestoreDirectory = true;
      this.openFileDialog1.Filter = "All Files|*.*|Excel Files|*.xls;*.xlsx";
      this.openFileDialog1.FilterIndex = 2;
      this.openFileDialog1.Title = "Select an Excel File to Upload...";
      this.openFileDialog1.FileName = "";
      if (this.openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
      {
        this.imprtTrnsTmp(this.openFileDialog1.FileName);
      }
      this.loadPanel();
    }

    private void imprtTrnsTmp(string filename)
    {
      this.obey_evnts = false;
      System.Windows.Forms.Application.DoEvents();
      Global.mnFrm.cmCde.clearPrvExclFiles();
      Global.mnFrm.cmCde.exclApp = new Microsoft.Office.Interop.Excel.Application();
      Global.mnFrm.cmCde.exclApp.WindowState = Excel.XlWindowState.xlNormal;
      Global.mnFrm.cmCde.exclApp.Visible = true;
      CommonCode.CommonCodes.SetWindowPos((IntPtr)Global.mnFrm.cmCde.exclApp.Hwnd, CommonCode.CommonCodes.HWND_TOP, 0, 0, 0, 0, CommonCode.CommonCodes.SWP_NOMOVE | CommonCode.CommonCodes.SWP_NOSIZE | CommonCode.CommonCodes.SWP_SHOWWINDOW);

      Global.mnFrm.cmCde.nwWrkBk = Global.mnFrm.cmCde.exclApp.Workbooks.Open(filename, 0, false, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "", true, false, 0, true, false, false);

      Global.mnFrm.cmCde.trgtSheets = new Excel.Worksheet[1];

      Global.mnFrm.cmCde.trgtSheets[0] = (Excel.Worksheet)Global.mnFrm.cmCde.nwWrkBk.Worksheets[1];
      string firmName = "";
      string firmDesc = "";
      string partnerType = "";
      string clsfctn = "";
      string liabltyAccNo = "";
      string rcvblAccNo = "";
      string estblsDate = "";
      string siteName = "";
      string bllngAddrs = "";
      string shpgAddrs = "";
      string cntctPrsns = "";
      string telNos = "";
      string emailNo = "";
      string wthldngTaxNm = "";
      string countryNm = "";
      string bnkName = "";
      string brnchName = "";
      string accNum = "";
      string crncyCode = "";
      string swftCode = "";
      string ibanNum = "";
      string cmpnyBrndNm = "";
      string typeOfOrg = "";
      string cmpnRegNum = "";
      string dateOfIncorp = "";
      string typeOfIncorp = "";
      string vatNum = "";
      string tinNum = "";
      string ssnitNum = "";
      string noofEmps = "";
      string descSrvcs = "";
      string listSrvcs = "";


      int rownum = 5;
      do
      {
        this.obey_evnts = false;
        try
        {
          firmName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 2]).Value2.ToString();
        }
        catch (Exception ex)
        {
          firmName = "";
        }
        try
        {
          firmDesc = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 3]).Value2.ToString();
        }
        catch (Exception ex)
        {
          firmDesc = "";
        }
        try
        {
          partnerType = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 4]).Value2.ToString();
        }
        catch (Exception ex)
        {
          partnerType = "";
        }
        try
        {
          clsfctn = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 5]).Value2.ToString();
        }
        catch (Exception ex)
        {
          clsfctn = "";
        }
        try
        {
          liabltyAccNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 6]).Value2.ToString();
        }
        catch (Exception ex)
        {
          liabltyAccNo = "";
        }
        try
        {
          rcvblAccNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 7]).Value2.ToString();
        }
        catch (Exception ex)
        {
          rcvblAccNo = "";
        }

        try
        {
          estblsDate = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 8]).Value2.ToString();
        }
        catch (Exception ex)
        {
          estblsDate = "";
        }
        try
        {
          siteName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 9]).Value2.ToString();
        }
        catch (Exception ex)
        {
          siteName = "";
        }
        try
        {
          bllngAddrs = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 10]).Value2.ToString();
        }
        catch (Exception ex)
        {
          bllngAddrs = "";
        }
        try
        {
          shpgAddrs = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 11]).Value2.ToString();
        }
        catch (Exception ex)
        {
          shpgAddrs = "";
        }
        try
        {
          cntctPrsns = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 12]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cntctPrsns = "";
        }
        try
        {
          telNos = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 13]).Value2.ToString();
        }
        catch (Exception ex)
        {
          telNos = "";
        }
        try
        {
          emailNo = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 14]).Value2.ToString();
        }
        catch (Exception ex)
        {
          emailNo = "";
        }
        try
        {
          wthldngTaxNm = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 15]).Value2.ToString();
        }
        catch (Exception ex)
        {
          wthldngTaxNm = "";
        }
        try
        {
          countryNm = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 16]).Value2.ToString();
        }
        catch (Exception ex)
        {
          countryNm = "";
        }
        try
        {
          bnkName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 17]).Value2.ToString();
        }
        catch (Exception ex)
        {
          bnkName = "";
        }
        try
        {
          brnchName = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 18]).Value2.ToString();
        }
        catch (Exception ex)
        {
          brnchName = "";
        }
        try
        {
          accNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 19]).Value2.ToString();
        }
        catch (Exception ex)
        {
          accNum = "";
        }
        try
        {
          crncyCode = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 20]).Value2.ToString();
        }
        catch (Exception ex)
        {
          crncyCode = "";
        }
        try
        {
          swftCode = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 21]).Value2.ToString();
        }
        catch (Exception ex)
        {
          swftCode = "";
        }
        try
        {
          ibanNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 22]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ibanNum = "";
        }
        try
        {
          cmpnyBrndNm = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 23]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cmpnyBrndNm = "";
        }
        try
        {
          typeOfOrg = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 24]).Value2.ToString();
        }
        catch (Exception ex)
        {
          typeOfOrg = "";
        }
        try
        {
          cmpnRegNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 25]).Value2.ToString();
        }
        catch (Exception ex)
        {
          cmpnRegNum = "";
        }
        try
        {
          dateOfIncorp = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 26]).Value2.ToString();
        }
        catch (Exception ex)
        {
          dateOfIncorp = "";
        }
        try
        {
          typeOfIncorp = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 27]).Value2.ToString();
        }
        catch (Exception ex)
        {
          typeOfIncorp = "";
        }
        try
        {
          vatNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 28]).Value2.ToString();
        }
        catch (Exception ex)
        {
          vatNum = "";
        }
        try
        {
          tinNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 29]).Value2.ToString();
        }
        catch (Exception ex)
        {
          tinNum = "";
        }
        try
        {
          ssnitNum = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 30]).Value2.ToString();
        }
        catch (Exception ex)
        {
          ssnitNum = "";
        }
        try
        {
          noofEmps = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 31]).Value2.ToString();
        }
        catch (Exception ex)
        {
          noofEmps = "";
        }
        try
        {
          descSrvcs = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 32]).Value2.ToString();
        }
        catch (Exception ex)
        {
          descSrvcs = "";
        }
        try
        {
          listSrvcs = ((Microsoft.Office.Interop.Excel.Range)Global.mnFrm.cmCde.trgtSheets[0].Cells[rownum, 33]).Value2.ToString();
        }
        catch (Exception ex)
        {
          listSrvcs = "";
        }
        if (rownum == 5)
        {
          string[] hdngs ={"Partner/Firm Name**","Partner/Firm Description","Partner Type(Customer/Supplier)**",
                        "Classification**", "Liability Account No.**","Receivables Account No.**", 
                        "Establishment Date**","Site Name**","Billing Address","Shipping Address",
                        "Contact Person**","Tel Nos.","Email","Withholding Tax Name", 
                        "Country**","Bank Name","Branch Name","Account Number",
                        "Currency Code","SWIFT/BIC CODE","IBAN Number",
                        "Company Brand Name","Type of Organisation","Company Registration Number",
                        "Date of Incorporation","Type of Incorporation","VAT Number","TIN Number",
                        "SSNIT No.","No. of Employees","Description of Services","List of Services"};

          if (firmName != hdngs[0].ToUpper()
            || rcvblAccNo != hdngs[5].ToUpper()
            || firmDesc != hdngs[1].ToUpper()
            || partnerType != hdngs[2].ToUpper()
            || liabltyAccNo != hdngs[4].ToUpper()
            || clsfctn != hdngs[3].ToUpper()
            || estblsDate != hdngs[6].ToUpper()
            || siteName != hdngs[7].ToUpper()
            || bllngAddrs != hdngs[8].ToUpper()
            || shpgAddrs != hdngs[9].ToUpper()
            || cntctPrsns != hdngs[10].ToUpper()
            || telNos != hdngs[11].ToUpper()
            || emailNo != hdngs[12].ToUpper()
            || wthldngTaxNm != hdngs[13].ToUpper()
            || countryNm != hdngs[14].ToUpper()
            || bnkName != hdngs[15].ToUpper()
            || brnchName != hdngs[16].ToUpper()
            || accNum != hdngs[17].ToUpper()
            || crncyCode != hdngs[18].ToUpper()
            || swftCode != hdngs[19].ToUpper()
            || ibanNum != hdngs[20].ToUpper()
            || cmpnyBrndNm != hdngs[21].ToUpper()
            || typeOfOrg != hdngs[22].ToUpper()
            || cmpnRegNum != hdngs[23].ToUpper()
            || dateOfIncorp != hdngs[24].ToUpper()
            || typeOfIncorp != hdngs[25].ToUpper()
            || vatNum != hdngs[26].ToUpper()
            || tinNum != hdngs[27].ToUpper()
            || ssnitNum != hdngs[28].ToUpper()
            || noofEmps != hdngs[29].ToUpper()
            || descSrvcs != hdngs[30].ToUpper()
            || listSrvcs != hdngs[31].ToUpper())
          {
            Global.mnFrm.cmCde.showMsg("The Excel File you Selected is not a Valid Template\r\nfor importing records here.", 0);
            return;
          }
          rownum++;
          continue;
        }
        if (firmName != "" && partnerType != ""
          && clsfctn != "" && liabltyAccNo != "" && rcvblAccNo != ""
          && estblsDate != "" && siteName != "" && cntctPrsns != "")
        {
          long prtnrID = Global.getCstmrSplrID(firmName, Global.mnFrm.cmCde.Org_id);
          
          double tstDte = 0;
          bool isdate = double.TryParse(estblsDate, out tstDte);
          if (isdate)
          {
            estblsDate = DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy");
          }
          tstDte = 0;
          isdate = double.TryParse(dateOfIncorp, out tstDte);
          if (isdate)
          {
            dateOfIncorp = DateTime.FromOADate(tstDte).ToString("dd-MMM-yyyy");
          }
          int lblAccID = Global.mnFrm.cmCde.getAccntID(liabltyAccNo, Global.mnFrm.cmCde.Org_id);
          int rcvblAccID = Global.mnFrm.cmCde.getAccntID(rcvblAccNo, Global.mnFrm.cmCde.Org_id);

          int accCurID = Global.mnFrm.cmCde.getPssblValID(crncyCode, Global.mnFrm.cmCde.getLovID("Currencies"));
          int empNo = 0;
          int.TryParse(noofEmps, out empNo);
          bool vldData = false;
          if (partnerType == "Customer"
            || partnerType == "Supplier"
            || partnerType == "Customer/Supplier")
          {
            if (Global.mnFrm.cmCde.getAccntType(lblAccID) == "L"
            && Global.mnFrm.cmCde.getAccntType(rcvblAccID) == "A"
            && Global.mnFrm.cmCde.isAccntContra(lblAccID) == "0"
            && Global.mnFrm.cmCde.isAccntContra(rcvblAccID) == "0")
            {
              vldData = true;
            }
          }

          if (prtnrID <= 0 && vldData == true)
          {
            Global.createCstSplrRec(Global.mnFrm.cmCde.Org_id,
              firmName, firmDesc, partnerType, clsfctn, lblAccID, rcvblAccID, -1, "Not Applicable",
              estblsDate, true, cmpnyBrndNm, typeOfOrg, cmpnRegNum, dateOfIncorp, typeOfIncorp,
              vatNum, tinNum, ssnitNum, empNo, descSrvcs, listSrvcs);
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":H" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            prtnrID = Global.getCstmrSplrID(firmName, Global.mnFrm.cmCde.Org_id);
            //rownum++;
            //continue;
            long siteID = Global.getCstmrSplrSiteID(siteName, prtnrID);
            if (siteID <= 0)
            {
              Global.createCstSplrSiteRec(prtnrID, siteName, siteName, cntctPrsns, telNos, emailNo, bnkName, brnchName, accNum, bllngAddrs, shpgAddrs,
                Global.getTaxID(wthldngTaxNm), -1, swftCode, countryNm, "", "", "", "", "", true, ibanNum, accCurID);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("I" + rownum + ":AG" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else
            {
              Global.updtCstSplrSiteRec(siteID, siteName, siteName, cntctPrsns, telNos, emailNo, bnkName, brnchName, accNum, bllngAddrs, shpgAddrs,
      Global.getTaxID(wthldngTaxNm), -1, swftCode, countryNm, "", "", "", "", "", true, ibanNum, accCurID);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("I" + rownum + ":AG" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 180, 0));
            }
          }
          else if (prtnrID > 0 && vldData == true)
          {
            Global.updtCstSplrRecExcl(prtnrID,
   firmName, firmDesc, partnerType, clsfctn, lblAccID, rcvblAccID,
   estblsDate, true, cmpnyBrndNm, typeOfOrg, cmpnRegNum, dateOfIncorp, typeOfIncorp,
   vatNum, tinNum, ssnitNum, empNo, descSrvcs, listSrvcs);
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":H" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 180, 0));
            long siteID = Global.getCstmrSplrSiteID(siteName, prtnrID);
            if (siteID <= 0)
            {
              Global.createCstSplrSiteRec(prtnrID, siteName, siteName, cntctPrsns, telNos, emailNo, bnkName, brnchName, accNum, bllngAddrs, shpgAddrs,
                Global.getTaxID(wthldngTaxNm), -1, swftCode, countryNm, "", "", "", "", "", true, ibanNum, accCurID);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("I" + rownum + ":AG" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 225, 0));
            }
            else
            {
              Global.updtCstSplrSiteRec(siteID, siteName, siteName, cntctPrsns, telNos, emailNo, bnkName, brnchName, accNum, bllngAddrs, shpgAddrs,
      Global.getTaxID(wthldngTaxNm), -1, swftCode, countryNm, "", "", "", "", "", true, ibanNum, accCurID);
              Global.mnFrm.cmCde.trgtSheets[0].get_Range("I" + rownum + ":AG" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(0, 180, 0));
            }
          }
          else
          {
            Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":H" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
          }
        }
        else
        {
          //Global.mnFrm.cmCde.trgtSheets[0].get_Range("A" + rownum + ":E" + rownum + "", Type.Missing).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(255, 0, 0));
          //this.trgtSheets[0].get_Range("M" + rownum + ":M" + rownum + "", Type.Missing).Value2 = errMsg;
        }

        rownum++;
      }
      while (firmName != "");
      this.obey_evnts = true;
    }

    private void accCurButton_Click(object sender, EventArgs e)
    {
      int[] selVals = new int[1];
      selVals[0] = int.Parse(this.accCurIDTextBox.Text);
      DialogResult dgRes = Global.mnFrm.cmCde.showPssblValDiag(
       Global.mnFrm.cmCde.getLovID("Currencies"), ref selVals,
       true, true);
      if (dgRes == DialogResult.OK)
      {
        for (int i = 0; i < selVals.Length; i++)
        {
          this.accCurIDTextBox.Text = selVals[i].ToString();
          this.accCurTextBox.Text = Global.mnFrm.cmCde.getPssblValNm(selVals[i]);
        }
      }
    }

  }
}