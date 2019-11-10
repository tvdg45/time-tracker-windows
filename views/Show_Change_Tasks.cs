using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timothys_Digital_Solutions_Time_Tracker.views
{
    public class Show_Change_Tasks
    {
        public static string Refresh_page()
        {
            string output = "";

            output += "<script type=\"text/javascript\">\n";
            output += "window.location = document.location.href.replace(\"#\", \"\");\n";
            output += "</script>\n";

            return output;
        }

        public static string Delete_tasks_error_message()
        {
            string output = "";

            output += "<br /><label><span style=\"color: red\">You must select a task before deleting any.</span></label>\n";

            return output;
        }
    }
}
