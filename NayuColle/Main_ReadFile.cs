using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace NayuColle
{
    public partial class Main
    {
        Dictionary<int, string> Kanmusu_Dic = new Dictionary<int, string>();

        void ReadFileToDic(string file, Dictionary<int, string> dic)
        {
            if (!System.IO.File.Exists(file))
            {
                textBox3.AppendText(file + "ファイルが見つかりません" + Environment.NewLine);
            }
            else
            {
                TextFieldParser parser = new TextFieldParser(file, System.Text.Encoding.GetEncoding("UTF-8"));
                parser.SetDelimiters(",");

                string[] data;
                while (!parser.EndOfData)
                {
                    data = parser.ReadFields();

                    dic.Add(int.Parse(data[0]), data[1]);
                }

            }
        }
    }
}
