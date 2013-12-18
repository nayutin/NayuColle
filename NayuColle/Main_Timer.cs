using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NayuColle
{
    public partial class Main
    {
     
        private void timer1_Tick(object sender, EventArgs e)
        {

            UpdateMission();
            UpdateDock();
        }
    }
}
