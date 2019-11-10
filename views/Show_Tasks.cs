using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using static Timothys_Digital_Solutions_Time_Tracker.utilities.Find_And_Replace;

namespace Timothys_Digital_Solutions_Time_Tracker.views
{
    public class Show_Tasks
    {
        //global variables
        public static List<List<string>> tasks = new List<List<string>>();

        public static string Show_tasks()
        {
            string output = "";

            List<string> find = new List<string>();
            List<string> replace = new List<string>();

            find.Add("<script");
            find.Add("<style");
            find.Add("\"");
            find.Add("'");
            find.Add("<br />");
            find.Add("<br>");
            find.Add("<div>");
            find.Add("</div>");

            replace.Add("&lt;script");
            replace.Add("&lt;style");
            replace.Add("&quot;");
            replace.Add("&apos;");
            replace.Add(" ");
            replace.Add("");
            replace.Add("");
            replace.Add("");

            output += "[";

            for (int i = 0; i < tasks[0].Count; i++)
            {
                output += "{\"row_id\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[0][i].ToString().Replace("\r\n", " ").Replace("<", "&lt;").Replace(">", "&gt;")) +
                        "\", \"user_id\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[1][i].ToString().Replace("\r\n", " ").Replace("<", "&lt;").Replace(">", "&gt;")) +
                        "\", \"task_name\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[2][i].ToString().Replace("\r\n", " ").Replace("<", "&lt;").Replace(">", "&gt;")) +
                        "\", \"description\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[3][i].ToString().Replace("\r\n", " ").Replace("<", "&lt;").Replace(">", "&gt;")) +
                        "\", \"time_started\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[4][i].ToString().Replace("\r\n", " ")) +
                        "\", \"time_stopped\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[5][i].ToString().Replace("\r\n", " ")) +
                        "\", \"date_received\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[6][i].ToString().Replace("\r\n", " ").Replace("<", "&lt;").Replace(">", "&gt;")) +
                        "\", \"time_received\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[7][i].ToString().Replace("\r\n", " ").Replace("<", "&lt;").Replace(">", "&gt;")) +
                        "\", \"total_time\": \"" +
                        utilities.Find_And_Replace.Find_and_replace(
                                find, replace, tasks[8][i].ToString().Replace("\r\n", " ").Replace("<", "&lt;").Replace(">", "&gt;")) +
                        "\"}, ";
            }

            output += "{}]";

            output = output.Replace(", {}", "");

            return output;
        }
    }
}