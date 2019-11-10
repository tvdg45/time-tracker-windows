using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timothys_Digital_Solutions_Time_Tracker.utilities
{
    public class Find_And_Replace
    {
        public static string Find_and_replace(List<string> find, List<string> replace, string input_value)
        {
            string output = "";

            for (int i = 0; i < find.Count; i++)
            {
                input_value = input_value.Replace(find[i].ToString(), replace[i].ToString());
            }

            output = input_value;

            return output;
        }
    }
}
