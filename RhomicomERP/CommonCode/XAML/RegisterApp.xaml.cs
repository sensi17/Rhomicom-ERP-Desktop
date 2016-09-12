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

namespace CommonCode.XAML
{
    /// <summary>
    /// Interaction logic for RegisterApp.xaml
    /// </summary>
    public partial class RegisterApp : Window
    {
        public RegisterApp()
        {
            InitializeComponent();
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
        private void registerProduct_Loaded(object sender, RoutedEventArgs e)
        {
            this.Title = "Rhomicom ERP " + CommonCode.CommonCodes.AppVersion;
            /*
             * 1. Use Name, Location, Email Address to form an encrypted url like https://portal.rhomicom.com/registerProduct.php?g=nbekyufg2i98103u;h1n3ei20en8923g23y2
             * 2. On that server decode the url and check and store company details in registerd customer's table
             * 3. return the support code comprising Name, Location, Email Address, Support Type, Start Date, End Date, Amount Billed, Amount Paid
             */
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
