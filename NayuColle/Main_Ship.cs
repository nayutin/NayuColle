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
        DataTable Member_Ship = new DataTable();

        /// <summary>
        /// 取得している艦娘のテーブルを作成するために、テーブルを初期化する
        /// </summary>
        private void Init_Member_ShipTable()
        {
            Member_Ship.Columns.Add("api_id");
            Member_Ship.Columns.Add("api_ship_id");
            Member_Ship.Columns.Add("api_cond");
            Member_Ship.Columns.Add("api_nowhp");
            Member_Ship.Columns.Add("api_maxhp");
            Member_Ship.Columns.Add("api_exp");
            Member_Ship.Columns.Add("api_lv");
            ds.Tables.Add(Member_Ship);
            Member_Ship.TableName = "mem_ship";
        }

        /// <summary>
        /// 取得している艦娘のテーブルを作成する。ship2、ship3を取得するたびに何度も書き換えが起こる
        /// </summary>
        /// <param name="ship_data"></param>
        void Make_Member_ShipTable(dynamic ship_data)
        {

            Member_Ship.Clear();
            foreach (var item in ((member_ship[])ship_data)
                .Select(p => new { p.api_id, p.api_ship_id, p.api_cond, p.api_nowhp, p.api_maxhp, p.api_exp, p.api_lv }))
            {
                DataRow row = Member_Ship.NewRow();
                row[0] = item.api_id;
                row[1] = item.api_ship_id;
                row[2] = item.api_cond;
                row[3] = item.api_nowhp;
                row[4] = item.api_maxhp;
                row[5] = item.api_exp[1];
                row[6] = item.api_lv;
                Member_Ship.Rows.Add(row);
            }
        }

        
    }
}
