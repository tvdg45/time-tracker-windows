using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timothys_Digital_Solutions_Time_Tracker.utilities
{
    public class Form_Validation
    {
        public static Boolean Is_string_null_or_whitespace(string input_string)
        {
            if (input_string == null)
            {
                return true;
            }

            for (int i = 0; i < input_string.Length; i++)
            {
                if (!Char.IsWhiteSpace(input_string[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}