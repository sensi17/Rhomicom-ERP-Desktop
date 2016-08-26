using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjectsManagement.Classes;

namespace ProjectsManagement.Forms
{
  public partial class wfnProjsSearchForm : WeifenLuo.WinFormsUI.Docking.DockContent
  {
    public wfnProjsSearchForm()
    {
      InitializeComponent();
    }
    #region "GLOBAL VARIABLES..."
    //Records;
    cadmaFunctions.NavFuncs myNav = new cadmaFunctions.NavFuncs();
    bool beenToCheckBx = false;

    long rec_cur_indx = 0;
    bool is_last_rec = false;
    long totl_rec = 0;
    long last_rec_num = 0;
    public string rec_SQL = "";
    public string recDt_SQL = "";
    bool obey_evnts = false;
    bool autoLoad = false;
    public bool txtChngd = false;
    public string srchWrd = "%";

    bool addRec = false;
    bool editRec = false;
    bool someLinesFailed = false;
    bool vwRecs = false;
    bool addRecs = false;
    bool editRecs = false;
    bool delRecs = false;

    //Line Dtails;
    long ldt_cur_indx = 0;
    bool is_last_ldt = false;
    long totl_ldt = 0;
    long last_ldt_num = 0;
    bool obey_ldt_evnts = false;
    public int curid = -1;
    public string curCode = "";
    public long appntmntID = -1;
    public long visitID = -1;
    public bool enblEdit = false;
    #endregion

    private void wfnProjsSearchForm_Load(object sender, EventArgs e)
    {

    }
 }
}
