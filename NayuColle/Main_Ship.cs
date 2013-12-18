using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Drawing;
using System.Diagnostics;

namespace NayuColle
{
    public partial class Main
    {
        DataTable ship3 = new DataTable();

        void MakeShip2_Table(dynamic ship3_data)
        {

            ship3.Clear();
            foreach (var item in ((member_ship3[])ship3_data)
                .Select(p => new { p.api_id, p.api_ship_id, p.api_cond, p.api_nowhp, p.api_maxhp, p.api_exp, p.api_lv }))
            {
                DataRow row = ship3.NewRow();
                row[0] = item.api_id;
                row[1] = item.api_ship_id;
                row[2] = item.api_cond;
                row[3] = item.api_nowhp;
                row[4] = item.api_maxhp;
                row[5] = item.api_exp[1];
                row[6] = item.api_lv;
                ship3.Rows.Add(row);
            }
        }

    }
}
