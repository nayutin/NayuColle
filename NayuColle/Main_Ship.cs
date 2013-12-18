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

        void Make_Member_ShipTable(dynamic ship3_data)
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

        private void Init_Member_ShipTable()
        {
            ship3.Columns.Add("api_id");
            ship3.Columns.Add("api_ship_id");
            ship3.Columns.Add("api_cond");
            ship3.Columns.Add("api_nowhp");
            ship3.Columns.Add("api_maxhp");
            ship3.Columns.Add("api_exp");
            ship3.Columns.Add("api_lv");
            ds.Tables.Add(ship3);
            ship3.TableName = "ship3";
        }
    }
}
