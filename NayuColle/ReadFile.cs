using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualBasic.FileIO;

namespace NayuColle
{
    class ReadFile
    {
        public string file;
        public Dictionary<int,string> Dic;

        public void ReadFileToDic()
        {
            if (!System.IO.File.Exists(file))
            {
                Console.WriteLine(file + "ファイルが見つかりません" + Environment.NewLine);
            }
            else
            {
                using (TextFieldParser parser = new TextFieldParser(file, System.Text.Encoding.GetEncoding("UTF-8")))
                {
                    parser.SetDelimiters(",");

                    string[] data;
                    while (!parser.EndOfData)
                    {
                        data = parser.ReadFields();
                        Dic.Add(int.Parse(data[0]), data[1]);
                    }

                }
            }
        }

    }
}
