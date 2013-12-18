using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace FCSample
{
    public partial class Main
    {

        void UpdateDeck(dynamic json)
        {

            foreach (var item in ((member_deck[])json)
                .Select(p => new { p.api_id, p.api_ship, p.api_name })
                )
            {
                if (checkBox3.Checked)
                    textBox3.AppendText(item.api_name + Environment.NewLine);

                fleet[(int)item.api_id - 1].Name = item.api_name;


                foreach (var ship in (item.api_ship)
                    .Select((value, index) => new { value, index })
                    )
                {
                    if ((double)ship.value > 0)
                    {
                        string filter = "api_id = '" + ship.value + "'";
                        DataRow[] selectship = ship3.Select(filter);
                        if (selectship.Count() == 1)
                        {
                            var shipid = Convert.ToInt32(selectship[0]["api_ship_id"]);
                            var cond = Convert.ToInt32(selectship[0]["api_cond"]);
                            var maxhp = Convert.ToInt32(selectship[0]["api_maxhp"]);
                            var nowhp = Convert.ToInt32(selectship[0]["api_nowhp"]);
                            var lv = Convert.ToInt32(selectship[0]["api_lv"]);
                            var exp = Convert.ToInt32(selectship[0]["api_exp"]);
                            //var exp = Exp_Dic.ContainsKey(lv + 1) ? (int.Parse(Exp_Dic[lv + 1]) - Convert.ToInt32(selectship[0]["api_exp"])) : 0;
                            var Name = Kanmusu_Dic.ContainsKey(shipid) ? Kanmusu_Dic[shipid] : "null";

                            if (checkBox3.Checked)
                                textBox3.AppendText(shipid + " " + Name + Environment.NewLine);

                            fleet[item.api_id - 1].kanmusu[ship.index] = Name;
                            fleet[item.api_id - 1].cond[ship.index] = cond;
                            fleet[item.api_id - 1].maxhp[ship.index] = maxhp;
                            fleet[item.api_id - 1].nowhp[ship.index] = nowhp;
                            fleet[item.api_id - 1].lv[ship.index] = lv;
                            fleet[item.api_id - 1].exp[ship.index] = exp;

                        }
                        else
                        {
                            fleet[(int)item.api_id - 1].kanmusu[ship.index] = "";
                            fleet[(int)item.api_id - 1].cond[ship.index] = 0;
                            fleet[(int)item.api_id - 1].maxhp[ship.index] = 0;
                            fleet[(int)item.api_id - 1].nowhp[ship.index] = 0;
                            fleet[item.api_id - 1].lv[ship.index] = 0;
                            fleet[item.api_id - 1].exp[ship.index] = 0;
                        }
                    }
                }

            }


            MakeFleet(fleetnum);
            switch (fleetnum)
            {
                case 0: Fleet1.ForeColor = Color.DodgerBlue;
                    break;
                case 1: Fleet2.ForeColor = Color.DodgerBlue;
                    break;
                case 2: Fleet3.ForeColor = Color.DodgerBlue;
                    break;
                case 3: Fleet4.ForeColor = Color.DodgerBlue;
                    break;
                default:
                    break;
            }


        }
    }
}
