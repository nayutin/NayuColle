using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NayuColle
{
    public partial class Main
    {
        DateTime[] DockTime = new DateTime[Constants.DOCK_MAX];

        void ParseDock(dynamic json)
        {
            foreach (var item in ((member_ndock[])json.api_data)
                .Select(p => new { p.api_complete_time, p.api_id })
                )
                DockTime[(int)item.api_id - 1] = new DateTime(1970, 1, 1, 0, 0, 0)
                    .AddMilliseconds(item.api_complete_time + 1000)
                    .ToLocalTime();
        }

        void UpdateDock()
        {
            this.Labels.Add(Dock1);
            this.Labels.Add(Dock2);
            this.Labels.Add(Dock3);
            this.Labels.Add(Dock4);

            TimeSpan[] Nokori = new TimeSpan[Constants.DOCK_MAX];

            foreach (var item in DockTime.Select((value, index) => new { value, index }))
            {
                var Now = System.DateTime.Now;

                if (DockTime[item.index] < Now)
                {
                    Labels[item.index].Text = "00:00:00";
                }
                else
                {
                    Nokori[item.index] = item.value - Now;
                    if (Nokori[item.index].TotalHours > 10)
                        Labels[item.index].Text = (int)Nokori[item.index].TotalHours + Nokori[item.index].ToString(@"\:mm\:ss");
                    else
                        Labels[item.index].Text = "0" + (int)Nokori[item.index].TotalHours + Nokori[item.index].ToString(@"\:mm\:ss");
                }
            }

            this.Labels.Clear();
        }

    }
}
