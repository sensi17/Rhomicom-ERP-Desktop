using mshtml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using CommonCode.WpfCore;
using System.Data;
using System.Net.Mail;
using System.Net;
using cadmaFunctions;

namespace CommonCode.XAML
{
    /// <summary>
    /// Interaction logic for sendMailNewDiag.xaml
    /// </summary>
    public partial class sendMailNewDiag : Window
    {
        public sendMailNewDiag()
        {
            InitializeComponent();
            System.Windows.Forms.Integration.ElementHost.EnableModelessKeyboardInterop(MainWindow1);
        }
        public int reportID = 0;
        public int sub_ID = 0;
        public int sub_sub_ID = 0;
        public int whoCalled = 0;
        public long prsnID = -1;
        public long[] prsnIDs = new long[1];
        public string[] cstmrIDs = new string[1];
        public string attcFiles = "";
        bool obey_evnts = false;
        public bool txtChngd = false;
        public string srchWrd = "%";

        //private cadmaFunctions.Encrypt encryptr = new Encrypt();
        public CommonCodes cmnCde = new CommonCodes();
        private void SettingsBold_Click(object sender, RoutedEventArgs e)
        {
            Format.bold();
        }

        private void SettingsItalic_Click(object sender, RoutedEventArgs e)
        {
            Format.Italic();
        }

        private void SettingsUnderLine_Click(object sender, RoutedEventArgs e)
        {
            Format.Underline();
        }

        private void SettingsRightAlign_Click(object sender, RoutedEventArgs e)
        {
            Format.Underline();
        }

        private void SettingsLeftAlign_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyLeft();
        }

        private void SettingsCenter2_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyCenter();
        }

        private void SettingsJustifyRight_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyRight();
        }

        private void SettingsJustifyFull_Click(object sender, RoutedEventArgs e)
        {
            Format.JustifyFull();
        }

        private void SettingsInsertOrderedList_Click(object sender, RoutedEventArgs e)
        {
            Format.InsertOrderedList();
        }

        private void SettingsBullets_Click(object sender, RoutedEventArgs e)
        {
            Format.InsertUnorderedList();
        }

        private void SettingsOutIdent_Click(object sender, RoutedEventArgs e)
        {
            Format.Outdent();
        }

        private void SettingsIdent_Click(object sender, RoutedEventArgs e)
        {
            Format.Indent();
        }

        private void RibbonButtonNew_Click(object sender, RoutedEventArgs e)
        {
            Gui.newdocument();
        }

        private void RibbonButtonOpen_Click(object sender, RoutedEventArgs e)
        {
            Gui.newdocumentFile();
        }

        private void RibbonButtonOpenweb_Click(object sender, RoutedEventArgs e)
        {
            webBrowserEditor.newWb(@"http://www.rhomicom.com/");
        }

        private void SettingsFontColor_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsFontColor();
        }

        private void SettingsBackColor_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsBackColor();
        }

        private void SettingsAddLink_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsAddLink();
        }

        private void SettingsAddImage_Click(object sender, RoutedEventArgs e)
        {
            Gui.SettingsAddImage();
        }

        private void RibbonButtonSave_Click(object sender, RoutedEventArgs e)
        {
            Gui.RibbonButtonSave();
        }

        private void RibbonComboboxFonts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFonts(RibbonComboboxFonts);
        }

        private void RibbonComboboxFontHeight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFontHeight(RibbonComboboxFontHeight);
        }

        private void RibbonComboboxFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gui.RibbonComboboxFormat(RibbonComboboxFormat);
        }

        private void EditWeb_Click(object sender, RoutedEventArgs e)
        {
            Gui.EditWeb();
        }

        private void ViewHTML_Click(object sender, RoutedEventArgs e)
        {
            Gui.ViewHTML();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Gui.webBrowser = webBrowserEditor;
            Gui.htmlEditor = HtmlEditor1;
            Initialisation1.webeditor = this;
            Gui.newdocument();

            Initialisation1.RibbonComboboxFontsInitialisation();
            Initialisation1.RibbonComboboxFontSizeInitialisation();
            Initialisation1.RibbonComboboxFormatInitionalisation();
            //this.bodyTextBox.fo
            //System.Drawing.Color[] clrs = cmnCde.getColors();
            //this.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, clrs[0].R, clrs[0].G, clrs[0].B));
            this.msgTypComboBox.SelectedIndex = 0;
            if (this.prsnID > 0)
            {
                if (this.grpComboBox.SelectedIndex < 0)
                {
                    this.grpComboBox.SelectedIndex = 7;
                }
            }
            this.grpSlctnComboBox.SelectedIndex = 0;
            this.attchMntsTextBox.Text = attcFiles.Replace(",", ";");
            this.mailLabel.Visibility = System.Windows.Visibility.Hidden;
            this.webBrowserEditor.Visibility = System.Windows.Visibility.Visible;
        }

        private void closeWindow_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void grpComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.grpNmIDTextBox.Text = "-1";
            this.grpNmTextBox.Text = "";
            string slctdText = (e.AddedItems[0] as ComboBoxItem).Content as string;
            //cmnCde.showSQLNoPermsn(slctdText);
            if (slctdText == "Everyone"
              || slctdText == "Currently Selected Person")
            {
                this.grpNmTextBox.IsReadOnly = true;
                this.grpNmButton.IsEnabled = false;
                this.grpNmIDTextBox.IsEnabled = false;
                this.grpNmTextBox.Background = new SolidColorBrush(
                  System.Windows.Media.Color.FromArgb(255, 240, 240, 240));

            }
            else
            {
                this.grpNmTextBox.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255, 255, 255, 118));
                this.grpNmTextBox.IsReadOnly = false;
                this.grpNmButton.IsEnabled = true;
                this.grpNmIDTextBox.IsEnabled = true;
            }
            if (this.prsnID > 0 && slctdText == "Currently Selected Person")
            {
                this.grpComboBox.SelectedItem = "Currently Selected Person";
                this.grpNmIDTextBox.Text = this.prsnID.ToString();
                this.grpNmTextBox.Text = cmnCde.getPrsnName(this.prsnID) + " (" + cmnCde.getPrsnLocID(this.prsnID) + ")";
            }
        }

        private void toTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key == Key.A) &&
             (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                TextBox mytxt = (TextBox)sender;
                mytxt.SelectAll();
            }
        }

        private void grpNmButton_Click(object sender, RoutedEventArgs e)
        {
            //Item Names
            if (this.grpComboBox.Text == "")
            {
                cmnCde.showMsg("Please select a Group Type!", 0);
                return;
            }
            string[] selVals = new string[1];
            selVals[0] = this.grpNmIDTextBox.Text;
            string grpCmbo = "";
            if (this.grpComboBox.Text == "Divisions/Groups")
            {
                grpCmbo = "Divisions/Groups";
            }
            else if (this.grpComboBox.Text == "Grade")
            {
                grpCmbo = "Grades";
            }
            else if (this.grpComboBox.Text == "Job")
            {
                grpCmbo = "Jobs";
            }
            else if (this.grpComboBox.Text == "Position")
            {
                grpCmbo = "Positions";
            }
            else if (this.grpComboBox.Text == "Site/Location")
            {
                grpCmbo = "Sites/Locations";
            }
            else if (this.grpComboBox.Text == "Person Type")
            {
                grpCmbo = "Person Types";
            }
            else if (this.grpComboBox.Text == "Working Hour Type")
            {
                grpCmbo = "Working Hours";
            }
            else if (this.grpComboBox.Text == "Gathering Type")
            {
                grpCmbo = "Gathering Types";
            }
            else if (this.grpComboBox.Text == "Companies/Institutions")
            {
                grpCmbo = "Schools/Organisations/Institutions";
            }
            else
            {
                grpCmbo = "Active Persons";
            }

            int[] selVal1s = new int[1];

            System.Windows.Forms.DialogResult dgRes;
            if (this.grpComboBox.Text != "Person Type"
              && this.grpComboBox.Text != "Companies/Institutions")
            {
                dgRes = cmnCde.showPssblValDiag(
                cmnCde.getLovID(grpCmbo), ref selVals, true, true, cmnCde.Org_id,
               this.srchWrd, "Both", true);
            }
            else
            {
                if (this.grpComboBox.Text == "Person Type")
                {
                    dgRes = cmnCde.showPssblValDiag(
                    cmnCde.getLovID("Person Types"), ref selVal1s, true, true,
                   this.srchWrd, "Both", true);
                }
                else
                {
                    dgRes = cmnCde.showPssblValDiag(
            cmnCde.getLovID(grpCmbo), ref selVal1s, false, true,
           this.srchWrd, "Both", true);
                }
            }
            int slctn = 0;
            if (this.grpComboBox.Text != "Person Type"
              && this.grpComboBox.Text != "Companies/Institutions")
            {
                slctn = selVals.Length;
            }
            else
            {
                slctn = selVal1s.Length;
            }
            if (dgRes == System.Windows.Forms.DialogResult.OK)
            {
                this.grpNmIDTextBox.Text = "-1";
                this.grpNmTextBox.Text = "";
                for (int i = 0; i < slctn; i++)
                {
                    if (this.grpComboBox.Text != "Person Type"
                 && this.grpComboBox.Text != "Companies/Institutions")
                    {
                        this.grpNmIDTextBox.Text = selVals[i];
                    }
                    if (this.grpComboBox.Text == "Divisions/Groups")
                    {
                        this.grpNmTextBox.Text = cmnCde.getDivName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Grade")
                    {
                        this.grpNmTextBox.Text = cmnCde.getGrdName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Job")
                    {
                        this.grpNmTextBox.Text = cmnCde.getJobName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Position")
                    {
                        this.grpNmTextBox.Text = cmnCde.getPosName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Site/Location")
                    {
                        this.grpNmTextBox.Text = cmnCde.getSiteName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Person Type")
                    {
                        this.grpNmIDTextBox.Text = selVal1s[i].ToString();
                        this.grpNmTextBox.Text = cmnCde.getPssblValNm(selVal1s[i]);
                    }
                    else if (this.grpComboBox.Text == "Companies/Institutions")
                    {
                        this.grpNmIDTextBox.Text += cmnCde.getGnrlRecID("scm.scm_cstmr_suplr",
                          "cust_sup_name", "cust_sup_id", cmnCde.getPssblValNm(selVal1s[i]), cmnCde.Org_id).ToString() + ";";
                        this.grpNmTextBox.Text += cmnCde.getPssblValNm(selVal1s[i]) + ";";
                        System.Windows.Forms.Application.DoEvents();
                    }
                    else if (this.grpComboBox.Text == "Working Hour Type")
                    {
                        this.grpNmTextBox.Text = cmnCde.getWkhName(int.Parse(selVals[i]));
                    }
                    else if (this.grpComboBox.Text == "Gathering Type")
                    {
                        this.grpNmTextBox.Text = cmnCde.getGathName(int.Parse(selVals[i]));
                    }
                    else
                    {
                        this.prsnID = cmnCde.getPrsnID(selVals[i]);
                        this.grpNmIDTextBox.Text = this.prsnID.ToString();
                        this.grpNmTextBox.Text = cmnCde.getPrsnName(this.prsnID) + " (" + selVals[i] + ")";
                    }
                }
            }
        }

        private void cstmrButton_Click(object sender, RoutedEventArgs e)
        {
            this.cstmrNmLOVSearch("%");
        }

        private void cstmrSiteButton_Click(object sender, RoutedEventArgs e)
        {
            this.cstmrSiteLOVSearch("%");
        }
        private void cstmrNmLOVSearch(string srchWrd)
        {
            this.txtChngd = false;

            if (!this.cstmrNmTextBox.Text.Contains("%"))
            {
                this.cstmrIDTextBox.Text = "-1";
            }

            string[] selVals = new string[1];
            selVals[0] = this.cstmrIDTextBox.Text;
            string extrWhr = " and tbl1.e <=0";
            System.Windows.Forms.DialogResult dgRes = cmnCde.showPssblValDiag(
             cmnCde.getLovID("All Customers and Suppliers"), ref selVals, true, false,
             cmnCde.Org_id, "", "",
             this.srchWrd, "Both", true, extrWhr);
            if (dgRes == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.cstmrIDTextBox.Text = selVals[i];
                    this.cstmrNmTextBox.Text = cmnCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr",
                      "cust_sup_id", "cust_sup_name", long.Parse(selVals[i]));
                    this.cstmrSiteIDTextBox.Text = "-1";
                    this.cstmrSiteTextBox.Text = "";
                }
            }
            this.txtChngd = false;
        }

        private void cstmrSiteLOVSearch(string srchWrd)
        {
            this.txtChngd = false;
            if (this.cstmrIDTextBox.Text == "" || this.cstmrIDTextBox.Text == "-1")
            {
                cmnCde.showMsg("Please pick a Workplace Name First!", 0);
                return;
            }
            if (!this.cstmrSiteTextBox.Text.Contains("%"))
            {
                this.cstmrSiteIDTextBox.Text = "-1";
            }

            string[] selVals = new string[1];
            selVals[0] = this.cstmrSiteIDTextBox.Text;
            System.Windows.Forms.DialogResult dgRes = cmnCde.showPssblValDiag(
              cmnCde.getLovID("Customer/Supplier Sites"), ref selVals,
              true, true, int.Parse(this.cstmrIDTextBox.Text),
             srchWrd, "Both", true);
            if (dgRes == System.Windows.Forms.DialogResult.OK)
            {
                for (int i = 0; i < selVals.Length; i++)
                {
                    this.cstmrSiteIDTextBox.Text = selVals[i];
                    this.cstmrSiteTextBox.Text = cmnCde.getGnrlRecNm(
                      "scm.scm_cstmr_suplr_sites", "cust_sup_site_id", "site_name",
                      long.Parse(selVals[i]));
                }
            }
            this.txtChngd = false;
        }

        private void button5_Click(object sender, RoutedEventArgs e)
        {
            string slctdText = this.grpComboBox.Text;
            string msgTyp = this.msgTypComboBox.Text;

            //cmnCde.showSQLNoPermsn(slctdText);
            if (slctdText != "Everyone"
        && slctdText != "Currently Selected Person")
            {
                if (this.grpNmIDTextBox.Text == "-1"
                || this.grpNmTextBox.Text == "")
                {
                    cmnCde.showMsg("Please select a Group Name!", 0);
                    return;
                }
            }
            if (msgTyp == "")
            {
                cmnCde.showMsg("Please select a Message Type!", 0);
                return;
            }
            System.Windows.Forms.Application.DoEvents();

            string curid = cmnCde.getOrgFuncCurID(cmnCde.Org_id).ToString();
            if (slctdText == "Companies/Institutions")
            {
                string[] spltChrs = { ";" };
                this.cstmrIDs = this.grpNmIDTextBox.Text.Split(spltChrs, StringSplitOptions.RemoveEmptyEntries);
                //int rwidx = 0;
                TextBlock nw = new TextBlock();
                nw.Text = "Loading the Companies/Institutions involved (" + this.cstmrIDs.Length + ") and their Contacts...Please Wait...";
                this.mailLabel.Content = nw;
                this.mailLabel.Visibility = System.Windows.Visibility.Visible;
                this.webBrowserEditor.Visibility = System.Windows.Visibility.Hidden;
                this.toTextBox.Text = "";
                for (int a = 0; a < this.cstmrIDs.Length; a++)
                {
                    //this.prsnID = this.prsnIDs[a];
                    if (msgTyp == "Email")
                    {
                        this.toTextBox.Text += cmnCde.getCstmrSpplrEmails(long.Parse(this.cstmrIDs[a])).Replace(",", ";") + ";";
                    }
                    else if (msgTyp == "SMS")
                    {
                        this.toTextBox.Text += cmnCde.getCstmrSpplrMobiles(long.Parse(this.cstmrIDs[a])).Replace(",", ";") + ";";
                    }
                    else
                    {
                        this.toTextBox.Text += this.cstmrIDs[a].Replace(",", ";") + ";";
                    }

                    nw = new TextBlock();
                    nw.Text = "Loading the Companies/Institutions involved (" + (a + 1).ToString() + "/" + this.cstmrIDs.Length + ") and their Contacts...Please Wait...";
                    this.mailLabel.Content = nw;
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            else
            {
                this.prsnIDs = this.getPrsnsInvolved();
                //int rwidx = 0;
                TextBlock nw = new TextBlock();
                nw.Text = "Loading the Persons involved (" + this.prsnIDs.Length + ") and their Contacts...Please Wait...";
                this.mailLabel.Content = nw;
                this.mailLabel.Visibility = System.Windows.Visibility.Visible;
                this.webBrowserEditor.Visibility = System.Windows.Visibility.Hidden;
                this.toTextBox.Text = "";
                for (int a = 0; a < this.prsnIDs.Length; a++)
                {
                    this.prsnID = this.prsnIDs[a];
                    if (msgTyp == "Email")
                    {
                        this.toTextBox.Text += cmnCde.getPrsnEmail(this.prsnID).Replace(",", ";") + ";";
                    }
                    else if (msgTyp == "SMS")
                    {
                        this.toTextBox.Text += cmnCde.getPrsnMobile(this.prsnID).Replace(",", ";") + ";";
                    }
                    else
                    {
                        this.toTextBox.Text += this.prsnIDs[a] + ";";
                    }
                    nw = new TextBlock();
                    nw.Text = "Loading the Persons involved (" + (a + 1).ToString() + "/" + this.prsnIDs.Length + ") and their Contacts...Please Wait...";
                    this.mailLabel.Content = nw;
                    System.Windows.Forms.Application.DoEvents();
                }
            }
            this.mailLabel.Visibility = System.Windows.Visibility.Hidden;
            this.webBrowserEditor.Visibility = System.Windows.Visibility.Visible;
        }

        private long[] getPrsnsInvolved()
        {
            string dateStr = cmnCde.getDB_Date_time();
            string extrWhr = "";

            string srchCrtr = this.grpSlctnComboBox.Text;
            if (srchCrtr == "contains")
            {
                srchCrtr = "%" + this.grpNmTextBox.Text.Replace("'", "''") + "%";
            }
            else if (srchCrtr == "is equal to")
            {
                srchCrtr = this.grpNmTextBox.Text.Replace("'", "''");
            }
            else
            {
                srchCrtr = this.grpNmTextBox.Text.Replace("'", "''") + "%";
            }

            if (long.Parse(this.cstmrIDTextBox.Text) > 0)
            {
                extrWhr += " and (Select distinct z.lnkd_firm_org_id From prs.prsn_names_nos z where z.person_id=a.person_id)=" + this.cstmrIDTextBox.Text;
            }
            if (long.Parse(this.cstmrSiteIDTextBox.Text) > 0)
            {
                extrWhr += " and (Select distinct z.lnkd_firm_site_id From prs.prsn_names_nos z where z.person_id=a.person_id)=" + this.cstmrSiteIDTextBox.Text;
            }

            string grpSQL = "";
            if (this.grpComboBox.Text == "Divisions/Groups")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_divs_groups a Where ((a.div_id IN " +
                    "(select z.div_id from org.org_divs_groups z where z.div_code_name ilike '" + srchCrtr + "')) and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Grade")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_grades a Where ((a.grade_id IN " +
                    "(select z.grade_id from org.org_grades z where z.grade_code_name ilike '" + srchCrtr + "')) and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Job")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_jobs a Where ((a.job_id IN " +
                    "(select z.job_id from org.org_jobs z where z.job_code_name ilike '" + srchCrtr + "')) and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Position")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_positions a Where ((a.position_id IN " +
                    "(select z.position_id from org.org_positions z where z.position_code_name ilike '" + srchCrtr + "')) and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Site/Location")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_locations a Where ((a.location_id IN " +
                    "(select z.location_id from org.org_sites_locations z where z.location_code_name ilike '" + srchCrtr + "')) and (to_timestamp('" + dateStr +
                  "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
                    "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            else if (this.grpComboBox.Text == "Person Type")
            {
                grpSQL = "Select distinct a.person_id From pasn.prsn_prsntyps a, prs.prsn_names_nos b " +
          "Where ((a.person_id = b.person_id) and (b.org_id = " + cmnCde.Org_id + ") and (a.prsn_type ilike '" +
          srchCrtr + "') and (to_timestamp('" + dateStr +
          "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            }
            //else if (this.grpComboBox.Text == "Working Hour Type")
            //{
            //    grpSQL = "Select distinct a.person_id From pasn.prsn_work_id a Where ((a.work_hour_id = " +
            //      int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
            //      "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            //        "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            //}
            //else if (this.grpComboBox.Text == "Gathering Type")
            //{
            //    grpSQL = "Select distinct a.person_id From pasn.prsn_gathering_typs a Where ((a.gatherng_typ_id = " +
            //      int.Parse(this.grpNmIDTextBox.Text) + ") and (to_timestamp('" + dateStr +
            //      "','YYYY-MM-DD HH24:MI:SS') between to_timestamp(a.valid_start_date|| ' 00:00:00','YYYY-MM-DD HH24:MI:SS') " +
            //        "AND to_timestamp(a.valid_end_date || ' 23:59:59','YYYY-MM-DD HH24:MI:SS'))" + extrWhr + ") ORDER BY a.person_id";
            //}
            //          
            else if (this.grpComboBox.Text == "Everyone")
            {
                grpSQL = "Select distinct a.person_id From prs.prsn_names_nos a Where ((a.org_id = "
                  + cmnCde.Org_id + ")" + extrWhr + ") ORDER BY a.person_id";
            }
            else
            {
                grpSQL = "Select distinct a.person_id From prs.prsn_names_nos a Where ((a.person_id = "
                  + this.grpNmIDTextBox.Text + ")" + extrWhr + ") ORDER BY a.person_id";
            }

            DataSet dtst = cmnCde.selectDataNoParams(grpSQL);
            this.prsnIDs = new long[dtst.Tables[0].Rows.Count];
            for (int i = 0; i < dtst.Tables[0].Rows.Count; i++)
            {
                this.prsnIDs[i] = long.Parse(dtst.Tables[0].Rows[i][0].ToString());
            }
            return this.prsnIDs;
        }

        private void ccButton_Click(object sender, RoutedEventArgs e)
        {
            getAddressesDiag nwDiag = new getAddressesDiag();
            nwDiag.selNamesTextBox.Text = "";
            nwDiag.selNamesTextBox.ReadOnly = true;
            nwDiag.selAddrsTextBox.Text = this.ccTextBox.Text;
            System.Windows.Forms.DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == System.Windows.Forms.DialogResult.OK)
            {
                this.ccTextBox.Text = nwDiag.selAddrsTextBox.Text;
            }
        }

        private void bccButton_Click(object sender, RoutedEventArgs e)
        {
            getAddressesDiag nwDiag = new getAddressesDiag();
            nwDiag.selNamesTextBox.Text = "";
            nwDiag.selNamesTextBox.ReadOnly = true;
            nwDiag.selAddrsTextBox.Text = this.bccTextBox.Text;
            System.Windows.Forms.DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == System.Windows.Forms.DialogResult.OK)
            {
                this.bccTextBox.Text = nwDiag.selAddrsTextBox.Text;
            }
        }

        private void browseButton_Click(object sender, RoutedEventArgs e)
        {
            //this.openFileDialog1.InitialDirectory = myComputer.FileSystem.SpecialDirectories.MyDocuments;
            using (System.Windows.Forms.OpenFileDialog openFileDialog1 = new System.Windows.Forms.OpenFileDialog())
            {
                openFileDialog1.FileName = "";
                openFileDialog1.RestoreDirectory = true;
                openFileDialog1.Filter = "All Files|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.Title = "Select a File to Attach";
                if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (this.attchMntsTextBox.Text == "")
                    {
                        this.attchMntsTextBox.Text = openFileDialog1.FileName;
                    }
                    else
                    {
                        this.attchMntsTextBox.AppendText(";" + openFileDialog1.FileName);
                    }
                }
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            //this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.msgTypComboBox.Text == "")
            {
                cmnCde.showMsg("Please select a Message Type!", 0);
                return;
            }
            if (this.toTextBox.Text.Replace(",", ";").Replace("\r\n", "").Replace(" ", "") == "")
            {
                MessageBox.Show("Receipient Address cannot be Empty!", "Rhomicom Message", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            string innerText = this.webBrowserEditor.doc.documentElement.innerText;
            string innerHtml = this.webBrowserEditor.doc.documentElement.innerHTML;
            if (innerText == "")
            {
                cmnCde.showMsg("Cannot Send an Empty Message!", 0);
                return;
            }
            //if (this.bodyTextBox.Text.Length > 160
            // && this.msgTypComboBox.Text == "SMS")
            //{
            //  cmnCde.showMsg("Your Number of Characters (" + this.bodyTextBox.Text.Length +
            //    ") Exceeds the Limit for SMS (160 Chars)", 0);
            //  return;
            //  //this.bodyTextBox.Text = this.bodyTextBox.Text.Substring(0, 160);
            //}
            if (this.msgTypComboBox.Text == "SMS")
            {
                this.toTextBox.Text = this.toTextBox.Text + ";" + this.ccTextBox.Text + ";" + this.bccTextBox.Text;
            }
            this.toTextBox.Text = this.toTextBox.Text.Replace(",", ";").Replace("\r\n", "");
            this.ccTextBox.Text = this.ccTextBox.Text.Replace(",", ";").Replace("\r\n", "");
            this.bccTextBox.Text = this.bccTextBox.Text.Replace(",", ";").Replace("\r\n", "");
            this.attchMntsTextBox.Text = this.attchMntsTextBox.Text.Replace(",", ";").Replace("\r\n", "");
            string errMsg = "";
            try
            {
                this.mailLabel.Visibility = System.Windows.Visibility.Visible;
                this.webBrowserEditor.Visibility = System.Windows.Visibility.Hidden;
                TextBlock nw = new TextBlock();
                nw.Text = "Sending Message...Please Wait...";
                this.mailLabel.Content = nw;
                System.Windows.Forms.Application.DoEvents();
                string[] spltChars = { ";" };
                char[] trmChars = { ';', ',' };
                string[] toEmails = this.toTextBox.Text.Replace(",", ";").Replace("\r\n", "").Split(spltChars, StringSplitOptions.RemoveEmptyEntries);
                //string[] ccEmails = this.ccTextBox.Text.Replace(",", ";").Split(spltChars, StringSplitOptions.RemoveEmptyEntries);
                //string[] bccEmails = this.bccTextBox.Text.Replace(",", ";").Split(spltChars, StringSplitOptions.RemoveEmptyEntries);
                //string[] attchMnts = this.attchMntsTextBox.Text.Replace(",", ";").Split(spltChars, StringSplitOptions.RemoveEmptyEntries);
                int cntrnLmt = 0;
                string mailLst = "";
                bool emlRes = false;
                string failedMails = "";
                //string errMsg = "";
                for (int i = 0; i < toEmails.Length; i++)
                {
                    if (cntrnLmt == 0)
                    {
                        mailLst = "";
                    }
                    mailLst += toEmails[i] + ",";
                    cntrnLmt++;
                    if ((cntrnLmt == 50 || i == toEmails.Length - 1
                      || this.sendIndvdllyCheckBox.IsChecked == true)
                      && this.msgTypComboBox.Text != "SMS")
                    {
                        //toEmails[i] mailLst.Trim(trmChars)
                        nw = new TextBlock();
                        nw.Text = "Sending Messages...(" + (i + 1).ToString() + "/" + toEmails.Length + ")...Please Wait...";
                        this.mailLabel.Content = nw;
                        System.Windows.Forms.Application.DoEvents();
                        if (this.msgTypComboBox.Text == "Email")
                        {
                            emlRes = cmnCde.sendEmail(
                              mailLst.Trim(trmChars),
                              this.ccTextBox.Text.Replace(",", ";"),
                              this.bccTextBox.Text.Replace(",", ";"),
                              this.attchMntsTextBox.Text.Replace(",", ";"),
                              this.subjTextBox.Text,
                              innerHtml,
                              ref errMsg);
                        }
                        else if (this.msgTypComboBox.Text == "SMS")
                        {
                        }
                        else
                        {

                        }
                        if (emlRes == false)
                        {
                            failedMails += mailLst.Trim(trmChars) + ";";
                        }
                        cntrnLmt = 0;
                    }
                    else if ((cntrnLmt == 1000 || i == toEmails.Length - 1)
                      && this.msgTypComboBox.Text == "SMS")
                    {
                        nw = new TextBlock();
                        nw.Text = "Sending Messages...(" + (i + 1).ToString() + "/" + toEmails.Length + ")...Please Wait...";
                        this.mailLabel.Content = nw;
                        System.Windows.Forms.Application.DoEvents();
                        emlRes = cmnCde.sendSMS(innerText,
            mailLst.Trim(trmChars), ref errMsg);
                        if (emlRes == false)
                        {
                            failedMails += mailLst.Trim(trmChars) + ";";
                        }
                        cntrnLmt = 0;
                    }
                }
                if (failedMails == "")
                {
                    cmnCde.showMsg("Message Successfully Sent to all Receipients!", 3);
                }
                else
                {
                    //errMsg +
                    cmnCde.showSQLNoPermsn("Messages to some Receipients Failed!\r\n" + "\r\n" + failedMails);
                }
                this.mailLabel.Visibility = System.Windows.Visibility.Hidden;
                this.webBrowserEditor.Visibility = System.Windows.Visibility.Visible;
            }
            catch (Exception ex)
            {
                this.mailLabel.Visibility = System.Windows.Visibility.Hidden;
                this.webBrowserEditor.Visibility = System.Windows.Visibility.Visible;
                MessageBox.Show(ex.Message, "Rhomicom Message", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void msgTypComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.grpNmIDTextBox.Text = "-1";
            this.grpNmTextBox.Text = "";

            if (this.grpComboBox.Text == "Everyone"
              || this.grpComboBox.Text == "Currently Selected Person")
            {
                this.grpNmTextBox.Background = Brushes.WhiteSmoke;
                this.grpNmTextBox.IsEnabled = false;
                this.grpNmButton.IsEnabled = false;
            }
            else
            {
                this.grpNmTextBox.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 118));
                this.grpNmTextBox.IsEnabled = true;
                this.grpNmButton.IsEnabled = true;
            }
            if (this.prsnID > 0 && this.grpComboBox.Text == "Currently Selected Person")
            {
                this.grpComboBox.SelectedItem = "Currently Selected Person";
                this.grpNmIDTextBox.Text = this.prsnID.ToString();
                this.grpNmTextBox.Text = cmnCde.getPrsnName(this.prsnID) + " (" + cmnCde.getPrsnLocID(this.prsnID) + ")";
            }
        }

        private void toButton_Click(object sender, RoutedEventArgs e)
        {
            getAddressesDiag nwDiag = new getAddressesDiag();
            nwDiag.selNamesTextBox.Text = "";
            nwDiag.selNamesTextBox.ReadOnly = true;
            nwDiag.selAddrsTextBox.Text = this.toTextBox.Text;
            System.Windows.Forms.DialogResult dgRes = nwDiag.ShowDialog();
            if (dgRes == System.Windows.Forms.DialogResult.OK)
            {
                this.toTextBox.Text = nwDiag.selAddrsTextBox.Text;
            }
        }

    }
}
