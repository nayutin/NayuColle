﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Codeplex.Data;
using System.Diagnostics;
using System.IO;
using System.Collections.ObjectModel;

namespace NayuColle
{
    public partial class Main : Form
    {
        delegate void UpdateUI();
        delegate void UpdateUI_JSON(dynamic json);


        // ラベル格納用コレクション。Marerialのラベルを編集する際に利用
        public Collection<Label> Labels = new Collection<Label>();

        //取得している艦娘のテーブル
        DataTable Member_Ship = new DataTable();
                
        //取得艦娘格納用ディクショナリ
        public Dictionary<int, string> Kanmusu_Dic = new Dictionary<int, string>();

        Stopwatch sw1 = new Stopwatch();
        Stopwatch sw2 = new Stopwatch();
        Stopwatch sw3 = new Stopwatch();
        Stopwatch sw4 = new Stopwatch();
        Stopwatch sw5 = new Stopwatch();

        DataSet ds = new DataSet();

        public Main()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Fiddler.FiddlerApplication.AfterSessionComplete += FiddlerApplication_AfterSessionComplete;
            Fiddler.FiddlerApplication.BeforeRequest += FiddlerApplication_BeforeRequest;
            Fiddler.FiddlerApplication.Startup(0, Fiddler.FiddlerCoreStartupFlags.Default);

            //タイマーの更新間隔を1秒に
            timer1.Interval = 1000;
            timer1.Start();

            //ラベルの格納 Main_Deckport.csで定義
            this.MissionTimeLabels.Add(Mission1);
            this.MissionTimeLabels.Add(Mission2);
            this.MissionTimeLabels.Add(Mission3);

            //ラベルの格納 Main_Dock.csで定義
            this.DockTimeLabels.Add(Dock1);
            this.DockTimeLabels.Add(Dock2);
            this.DockTimeLabels.Add(Dock3);
            this.DockTimeLabels.Add(Dock4);

            for (int i = 0; i < Constants.FLEET_MAX; i++)
                fleet[i] = new Fleet();

           // Init_Member_ShipTable();
            var Member = new Edit_Ship { Ships = Member_Ship };
            Member.Init_Member_ShipTable();
            ds.Tables.Add(Member_Ship);
            Member_Ship.TableName = "mem_ship";

            var read = new ReadFile { file = "kanmusu.csv", Dic = Kanmusu_Dic };
            read.ReadFileToDic();
        }

        /// <summary>
        /// URLフィルタ
        /// 必要のないURLにIgroneフラグを立てる
        /// </summary>
        /// <param name="oSession"></param>
        void FiddlerApplication_BeforeRequest(Fiddler.Session oSession)
        {
            string url = oSession.fullUrl;
            if (url.IndexOf("kcs") < 0 || url.IndexOf("kcsapi") < 0)
                oSession.Ignore();
        }

        void FiddlerApplication_AfterSessionComplete(Fiddler.Session oSession)
        {
            //ignoreフラグが立っていればリターン
            if (oSession.isFlagSet(Fiddler.SessionFlags.Ignored))
                return;

            string url = oSession.fullUrl;
            try
            {

                Invoke(new UpdateUI(() =>
                   {
                       if (checkBox1.Checked)
                       {
                           if (url.IndexOf("/kcs/") > 0)
                           {
                               listBox1.Items.Add(url);
                               listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;
                           }

                           if (url.IndexOf("/kcsapi/") > 0)
                           {
                               if (checkBox2.Checked)
                                   sw5.Start();

                               listBox1.Items.Add(url);
                               listBox1.TopIndex = listBox1.Items.Count - listBox1.Height / listBox1.ItemHeight;

                               if (checkBox4.Checked)
                               {
                                   string str = oSession.GetResponseBodyAsString();
                                   str = str.Substring(str.IndexOf("=") + 1);
                                   var json = DynamicJson.Parse(str);
                                   string filepath = @"result.txt";
                                   string text = url + Environment.NewLine + str + Environment.NewLine + json + Environment.NewLine + Environment.NewLine;
                                   File.AppendAllText(filepath, text);
                               }

                               if (checkBox3.Checked)
                               {
                                   string str = oSession.GetResponseBodyAsString();
                                   str = str.Substring(str.IndexOf("=") + 1);
                                   var json = DynamicJson.Parse(str);
                                   textBox1.AppendText(url + Environment.NewLine + str + Environment.NewLine + Environment.NewLine);
                                   textBox2.AppendText(url + Environment.NewLine + json + Environment.NewLine + Environment.NewLine);
                               }
                           }

                       }




                   }));
                if (url.IndexOf("/kcsapi/") > 0)
                {

                    string str = oSession.GetResponseBodyAsString();
                    str = str.Substring(str.IndexOf("=") + 1);
                    var json = DynamicJson.Parse(str);
                    var Member = new Edit_Ship { Ships = Member_Ship };

                    if (url.IndexOf("/api_get_member/basic") > 0)
                        MakeBasic(json);

                    if (url.IndexOf("/api_get_member/deck_port") > 0)
                        ParseMission(json);

                    if (url.IndexOf("/api_get_member/ndock") > 0)
                        ParseDock(json);

                    if (url.IndexOf("/api_get_member/material") > 0)
                        MakeMaterial(json);


                    if (url.IndexOf("api_get_member/deck") > 0 && url.IndexOf("api_get_member/deck_port") < 0)
                    {
                        var json_deck = json.api_data;
                        ParseMission(json);
                        Invoke(new UpdateUI_JSON(UpdateDeck), json_deck);
                    }

                    if (url.IndexOf("/api_get_member/ship2") > 0)
                    {

                        var deck_data = json.api_data_deck;
                        var ship_data = json.api_data;
                        var loop = ((member_ship[])ship_data).Count();

                        KanmusuCurrent.Invoke(new UpdateUI(() =>
                        {
                            KanmusuCurrent.Text = loop.ToString();
                        }));
                        
                        Member.Make_Member_ShipTable(ship_data);
                       // Make_Member_ShipTable(ship_data);
                        Invoke(new UpdateUI_JSON(UpdateDeck), deck_data);
                    }

                    if (url.IndexOf("/api_get_member/ship3") > 0)
                    {
                        var deck_data = json.api_data.api_deck_data;
                        var ship_data = json.api_data.api_ship_data;
                        var loop = ((member_ship[])ship_data).Count();

                        KanmusuCurrent.Invoke(new UpdateUI(() =>
                        {
                            KanmusuCurrent.Text = loop.ToString();
                        }));
                        Member.Make_Member_ShipTable(ship_data);
                       // Make_Member_ShipTable(ship_data);
                        Invoke(new UpdateUI_JSON(UpdateDeck), deck_data);
                    }



                    sw5.Stop();
                    if (checkBox2.Checked)
                    {
                        textBox3.Invoke(new UpdateUI(() =>
                        {
                            textBox3.AppendText("\"" + url + "\"" + Environment.NewLine + "Session Complete = " + sw5.Elapsed + Environment.NewLine);
                        }));
                    }
                    sw5.Reset();
                }
            }
            catch
            {
                textBox3.Invoke(new UpdateUI(() =>
                {
                    textBox3.AppendText(url + "にて例外エラー" + Environment.NewLine);
                }));
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
                this.Height = 835;
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
                listBox1.Items.Clear();
                this.Height = 345;
                checkBox3.CheckState = CheckState.Unchecked;
                checkBox2.CheckState = CheckState.Unchecked;
            }

        }


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Fiddler.FiddlerApplication.Shutdown();
        }

    }
}
