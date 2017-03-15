using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Enterprise_Management_System.Forms;
using System.Threading;

namespace Enterprise_Management_System.Classes
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.ThreadException += new ThreadExceptionEventHandler(
  Application_ThreadException);

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new mainForm());
    }

    public static void Application_ThreadException(
object sender, ThreadExceptionEventArgs e)
    {
      // Handle exception. 
      // The exception object is contained in e.Exception.
      // e.Exception.GetType().ToString() + "\r\n" +
      MessageBox.Show(e.Exception.Message + "\r\n" + e.Exception.InnerException + "\r\n" + e.Exception.StackTrace/**/, 
        "System Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

  }
}