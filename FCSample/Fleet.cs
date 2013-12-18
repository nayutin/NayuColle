using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FCSample
{
    class Fleet
    {
            public string Name { get; set; }
            public string[] kanmusu = new string[Constants.KANMUSU];
            public int[] cond = new int[Constants.KANMUSU];
            public int[] nowhp = new int[Constants.KANMUSU];
            public int[] maxhp = new int[Constants.KANMUSU];
            public int[] lv = new int[Constants.KANMUSU];
            public int[] exp = new int[Constants.KANMUSU];

        
    }
}
