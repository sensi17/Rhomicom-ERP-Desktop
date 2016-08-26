using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace EventsAndAttendance.Classes
{
  class MyDataGridView : DataGridView
  {
    protected override bool ProcessDialogKey(Keys keyData)
    {
      try
      {
        if (keyData == Keys.Enter || keyData == Keys.Return)
        {
          base.ProcessTabKey(Keys.Tab);
          return true;
        }
        return base.ProcessDialogKey(keyData);
      }
      catch (Exception)
      {
        return true;
      }
    }

    protected override bool ProcessDataGridViewKey(KeyEventArgs e)
    {
      try
      {
        if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
        {
          base.ProcessTabKey(Keys.Tab);
          return true;
        }
        return base.ProcessDataGridViewKey(e);
      }
      catch (Exception)
      {
        return true;
      }
    }
  }
}
