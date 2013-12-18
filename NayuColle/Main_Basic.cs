using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace NayuColle
{
    public partial class Main
    {

        void MakeBasic(dynamic json)
        {
            this.Labels.Add(Material1);
            this.Labels.Add(Material2);
            this.Labels.Add(Material3);
            this.Labels.Add(Material4);

            var data = (member_basic)json.api_data;
            Invoke(new UpdateUI(() =>
            {
                KanmusuMax.Text = (data.api_max_chara).ToString();
            }));
            var Mat_max = Constants.INIT_MATERIAL + data.api_level * Constants.MATERIAL_INCREASE;
            foreach (var item in Labels.Select((value, index) => new { value, index }))
            {
                if (item.index > 3) break;
                if (int.Parse(item.value.Text) > Mat_max)
                    item.value.ForeColor = Color.DodgerBlue;
                else
                    item.value.ForeColor = SystemColors.ControlText;
            }

            this.Labels.Clear();
        }


    }
}
