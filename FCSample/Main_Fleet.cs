using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace FCSample
{
    public partial class Main
    {
        List<Label> NameLabel = new List<Label>(Constants.KANMUSU);
        List<Label> CondLabel = new List<Label>(Constants.KANMUSU);
        List<Label> HPLabel = new List<Label>(Constants.KANMUSU);
        List<Label> LvLabel = new List<Label>(Constants.KANMUSU);
        List<Label> ExpLabel = new List<Label>(Constants.KANMUSU);
        Fleet[] fleet = new Fleet[Constants.FLEET_MAX];                 
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

        void MakeFleet(int fleetnum)
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


            FleetName.Text = fleet[fleetnum].Name;
            for (int i = 0; i < Constants.KANMUSU; i++)
            {
                if (fleet[fleetnum].kanmusu[i] != "")
                {
                    NameLabel[i].Visible = true;
                    CondLabel[i].Visible = true;
                    HPLabel[i].Visible = true;
                    LvLabel[i].Visible = true;
                    ExpLabel[i].Visible = true;
                    NameLabel[i].Text = fleet[fleetnum].kanmusu[i];
                    CondLabel[i].Text = fleet[fleetnum].cond[i].ToString();
                    HPLabel[i].Text = fleet[fleetnum].nowhp[i].ToString() + " / " + fleet[fleetnum].maxhp[i].ToString();
                    LvLabel[i].Text = fleet[fleetnum].lv[i].ToString();
                    ExpLabel[i].Text = fleet[fleetnum].exp[i].ToString();

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

                    if (fleet[fleetnum].cond[i] >= 50)
                        CondLabel[i].BackColor = Color.Yellow;
                    else if (fleet[fleetnum].cond[i] < 50 && fleet[fleetnum].cond[i] >= 30)
                        CondLabel[i].BackColor = Color.Transparent;
                    else if (fleet[fleetnum].cond[i] < 30 && fleet[fleetnum].cond[i] >= 20)
                        CondLabel[i].BackColor = Color.Orange;
                    else
                        CondLabel[i].BackColor = Color.Red;
                }
                else
                {
                    NameLabel[i].Visible = false;
                    CondLabel[i].Visible = false;
                    HPLabel[i].Visible = false;
                    LvLabel[i].Visible = false;
                    ExpLabel[i].Visible = false;
                }
            }
        }
    }
}
