using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace NayuColle
{
    public partial class Main
    {
        List<Label> NameLabel = new List<Label>(Constants.KANMUSU);
        List<Label> CondLabel = new List<Label>(Constants.KANMUSU);
        List<Label> HPLabel = new List<Label>(Constants.KANMUSU);
        List<Label> LvLabel = new List<Label>(Constants.KANMUSU);
        List<Label> ExpLabel = new List<Label>(Constants.KANMUSU);
        Fleet[] fleet = new Fleet[Constants.FLEET_MAX];


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

        public static int fleetnum = 0;

        private void Fleet1_Click(object sender, EventArgs e)
        {
            fleetnum = 0;
            MakeFleet(fleetnum);
            Fleet1.ForeColor = Color.DodgerBlue;
            Fleet2.ForeColor = SystemColors.ControlText;
            Fleet3.ForeColor = SystemColors.ControlText;
            Fleet4.ForeColor = SystemColors.ControlText;
        }

        private void Fleet2_Click(object sender, EventArgs e)
        {
            fleetnum = 1;
            MakeFleet(fleetnum);
            Fleet1.ForeColor = SystemColors.ControlText;
            Fleet2.ForeColor = Color.DodgerBlue;
            Fleet3.ForeColor = SystemColors.ControlText;
            Fleet4.ForeColor = SystemColors.ControlText;
        }

        private void Fleet3_Click(object sender, EventArgs e)
        {
            fleetnum = 2;
            MakeFleet(fleetnum);
            Fleet1.ForeColor = SystemColors.ControlText;
            Fleet2.ForeColor = SystemColors.ControlText;
            Fleet3.ForeColor = Color.DodgerBlue;
            Fleet4.ForeColor = SystemColors.ControlText;
        }

        private void Fleet4_Click(object sender, EventArgs e)
        {
            fleetnum = 3;
            MakeFleet(fleetnum);
            Fleet1.ForeColor = SystemColors.ControlText;
            Fleet2.ForeColor = SystemColors.ControlText;
            Fleet3.ForeColor = SystemColors.ControlText;
            Fleet4.ForeColor = Color.DodgerBlue;

        }
        /// <summary>
        /// 艦隊データの表示
        /// </summary>
        /// <param name="fleetnum">艦隊の番数</param>
        void MakeFleet(int fleetnum)
        {
            Init_KanmusuLabel();

            FleetName.Text = fleet[fleetnum].Name;

            for (int i = 0; i < Constants.KANMUSU; i++)
            {
                if (fleet[fleetnum].kanmusu[i] != "")
                {
                    Visible_KanmusuLabel(i);
                    Update_KanmusuLabel(fleetnum, i);
                    Display_HPandStatus(fleetnum, i);
                    Cond_ColorChange(fleetnum, i);
                }
                else
                {
                    Invisible_KanmusuLabel(i);
                }
            }
        }

        /// <summary>
        /// 艦娘のステータスラベルの初期化
        /// </summary>
        private void Init_KanmusuLabel()
        {
            this.NameLabel.Add(name1);
            this.NameLabel.Add(name2);
            this.NameLabel.Add(name3);
            this.NameLabel.Add(name4);
            this.NameLabel.Add(name5);
            this.NameLabel.Add(name6);

            this.CondLabel.Add(cond1);
            this.CondLabel.Add(cond2);
            this.CondLabel.Add(cond3);
            this.CondLabel.Add(cond4);
            this.CondLabel.Add(cond5);
            this.CondLabel.Add(cond6);

            this.HPLabel.Add(hp1);
            this.HPLabel.Add(hp2);
            this.HPLabel.Add(hp3);
            this.HPLabel.Add(hp4);
            this.HPLabel.Add(hp5);
            this.HPLabel.Add(hp6);

            this.LvLabel.Add(lv1);
            this.LvLabel.Add(lv2);
            this.LvLabel.Add(lv3);
            this.LvLabel.Add(lv4);
            this.LvLabel.Add(lv5);
            this.LvLabel.Add(lv6);

            this.ExpLabel.Add(exp1);
            this.ExpLabel.Add(exp2);
            this.ExpLabel.Add(exp3);
            this.ExpLabel.Add(exp4);
            this.ExpLabel.Add(exp5);
            this.ExpLabel.Add(exp6);
        }


        /// <summary>
        /// 艦娘のステータス更新
        /// </summary>
        /// <param name="fleetnum">艦隊の番数</param>
        /// <param name="i">編成番号</param>
        private void Update_KanmusuLabel(int fleetnum, int i)
        {
            NameLabel[i].Text = fleet[fleetnum].kanmusu[i];
            CondLabel[i].Text = fleet[fleetnum].cond[i].ToString();
            HPLabel[i].Text = fleet[fleetnum].nowhp[i].ToString() + " / " + fleet[fleetnum].maxhp[i].ToString();
            LvLabel[i].Text = fleet[fleetnum].lv[i].ToString();
            ExpLabel[i].Text = fleet[fleetnum].exp[i].ToString();
        }

        /// <summary>
        /// 艦娘が艦隊に存在する場合のステータスの表示
        /// </summary>
        /// <param name="i">編成番号</param>
        private void Visible_KanmusuLabel(int i)
        {
            NameLabel[i].Visible = true;
            CondLabel[i].Visible = true;
            HPLabel[i].Visible = true;
            LvLabel[i].Visible = true;
            ExpLabel[i].Visible = true;
        }

        /// <summary>
        /// 艦娘が艦隊に存在しない場合ステータスを非表示
        /// </summary>
        /// <param name="i">編成番号</param>
        private void Invisible_KanmusuLabel(int i)
        {
            NameLabel[i].Visible = false;
            CondLabel[i].Visible = false;
            HPLabel[i].Visible = false;
            LvLabel[i].Visible = false;
            ExpLabel[i].Visible = false;
        }

        /// <summary>
        /// HPの残量で艦娘の被害状況を変更
        /// </summary>
        /// <param name="fleetnum">艦隊の番数</param>
        /// <param name="i">編成番号</param>
        private void Display_HPandStatus(int fleetnum, int i)
        {
            if ((double)fleet[fleetnum].nowhp[i] > (double)fleet[fleetnum].maxhp[i] * 3.0 / 4.0)
                HPLabel[i].BackColor = Color.Transparent;
            else if ((double)fleet[fleetnum].nowhp[i] <= (double)fleet[fleetnum].maxhp[i] * 3.0 / 4.0 && (double)fleet[fleetnum].nowhp[i] > (double)fleet[fleetnum].maxhp[i] / 2.0)
            {
                HPLabel[i].BackColor = Color.Transparent;
                HPLabel[i].Text = fleet[fleetnum].nowhp[i].ToString() + " / " + fleet[fleetnum].maxhp[i].ToString() + " [小破]";
            }

            else if ((double)fleet[fleetnum].nowhp[i] <= (double)fleet[fleetnum].maxhp[i] / 2.0 && (double)fleet[fleetnum].nowhp[i] > (double)fleet[fleetnum].maxhp[i] / 4.0)
            {
                HPLabel[i].Text = fleet[fleetnum].nowhp[i].ToString() + " / " + fleet[fleetnum].maxhp[i].ToString() + " [中破]";
                HPLabel[i].BackColor = Color.Orange;
            }
            else
            {
                HPLabel[i].Text = fleet[fleetnum].nowhp[i].ToString() + " / " + fleet[fleetnum].maxhp[i].ToString() + " [大破]";
                HPLabel[i].BackColor = Color.Red;
            }
        }

        /// <summary>
        /// 疲労の色表示
        /// </summary>
        /// <param name="fleetnum">艦隊の番数</param>
        /// <param name="i">編成番号</param>
        private void Cond_ColorChange(int fleetnum, int i)
        {
            if (fleet[fleetnum].cond[i] >= 50)
                CondLabel[i].BackColor = Color.Yellow;
            else if (fleet[fleetnum].cond[i] < 50 && fleet[fleetnum].cond[i] >= 30)
                CondLabel[i].BackColor = Color.Transparent;
            else if (fleet[fleetnum].cond[i] < 30 && fleet[fleetnum].cond[i] >= 20)
                CondLabel[i].BackColor = Color.Orange;
            else
                CondLabel[i].BackColor = Color.Red;
        }
    }
}
