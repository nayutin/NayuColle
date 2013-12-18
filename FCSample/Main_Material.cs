using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Forms;

namespace FCSample
{
    public partial class Main
    {

       

        void MakeMaterial(dynamic json)
        {
            this.Labels.Add(Material1);
            this.Labels.Add(Material2);
            this.Labels.Add(Material3);
            this.Labels.Add(Material4);
            this.Labels.Add(Material5);
            this.Labels.Add(Material6);
            this.Labels.Add(Material7);

            foreach (var item in ((member_material[])json.api_data)
                .Select(p => new { p.api_value, p.api_id })
                )
                Invoke(new UpdateUI (() => {
                    Labels[item.api_id - 1].Text = (item.api_value).ToString();
                }));

            this.Labels.Clear();
        }


    }
}
