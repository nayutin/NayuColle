using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using System.Collections.ObjectModel;

namespace NayuColle
{
    public partial class Main
    {
        DateTime[] MissionTime = new DateTime[Constants.FLEET_MAX];
        Collection<Label> MissionTimeLabels = new Collection<Label>();

        private static int[] Alert_Flag = {-1,-1,-1,-1};


        /// <summary>
        /// JSONから取得した遠征の時間をパースする
        /// </summary>
        /// <param name="json">JSONデータ</param>
        void ParseMission(dynamic json)
        {
            foreach (var item in ((member_deck[])json.api_data)
                .Select(p => new { p.api_mission, p.api_id })
                )
            {
                MissionTime[item.api_id - 1] = new DateTime(1970, 1, 1, 0, 0, 0)
                    .AddMilliseconds(item.api_mission[2])
                    .ToLocalTime();
            }
        }


        /// <summary>
        /// 遠征時間の更新をする
        /// 遠征終了時にダイアログ表示
        /// 1分以内になれば時間の背景を黄色に
        /// </summary>
        void UpdateMission()
        {
           
            TimeSpan[] Nokori = new TimeSpan[Constants.FLEET_MAX - 1];
            var Now = System.DateTime.Now;

            this.MissionTimeLabels.Add(Mission1);
            this.MissionTimeLabels.Add(Mission2);
            this.MissionTimeLabels.Add(Mission3);

            foreach (var item in MissionTime.Select((value, index) => new { value, index }))
            {
                if (item.index == 0) continue;
                Nokori[item.index - 1] = item.value - Now;
                if (item.value <= Now)
                {
                    MissionTimeLabels[item.index - 1].Text = "00:00:00";
                     MissionTimeLabels[item.index - 1].BackColor = Color.Transparent;
                    if (Alert_Flag[item.index] == 0 && item.value.Year != 1970)
                    {
                        Alert_Flag[item.index] = 1;
                        MissionEndAlert();
                    }

                }
                else
                {
                    Alert_Flag[item.index] = 0;
                    if (Nokori[item.index - 1].TotalHours > 10)
                        MissionTimeLabels[item.index - 1].Text = (int)Nokori[item.index - 1].TotalHours + Nokori[item.index - 1].ToString(@"\:mm\:ss");
                    else
                        MissionTimeLabels[item.index - 1].Text = "0" + (int)Nokori[item.index - 1].TotalHours + Nokori[item.index - 1].ToString(@"\:mm\:ss");
 
                    if (Nokori[item.index - 1].TotalSeconds < 60)
                        MissionTimeLabels[item.index - 1].BackColor = Color.Orange;   
                }

            }
            this.MissionTimeLabels.Clear();

        }
        /// <summary>
        /// ダイアログを表示する関数
        /// </summary>
        private void MissionEndAlert()
        {
            Alert Alrt = new Alert();
            Alrt.ShowDialog(this);
            Alrt.Dispose();
        }
    }
}
