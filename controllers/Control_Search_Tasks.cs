using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static Timothys_Digital_Solutions_Time_Tracker.views.Show_Tasks;
using static Timothys_Digital_Solutions_Time_Tracker.models.Search_Tasks;
using static Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation;

namespace Timothys_Digital_Solutions_Time_Tracker.controllers
{
    public class Control_Search_Tasks : models.Search_Tasks
    {
        //global variables
        public static MySql.Data.MySqlClient.MySqlConnection use_connection;
        public static string task_owner;
        public static List<List<string>> search_tasks;

        public static string Request_tasks()
        {
            string output;

            if (!(utilities.Form_Validation.Is_string_null_or_whitespace(task_owner)))
            {
                try
                {
                    connection = use_connection;

                    Set_task_owner(task_owner);

                    search_tasks = Search_tasks();

                    if (Convert.ToString(search_tasks[1][0]).Equals("no task")
                        || Convert.ToString(search_tasks[1][0]).Equals("fail"))
                    {
                        output = "no tasks";
                    }
                    else
                    {
                        views.Show_Tasks.tasks = search_tasks;

                        output = views.Show_Tasks.Show_tasks();
                    }
                }
                catch (Exception)
                {
                    output = "[{\"row_id\": \"\", \"user_id\": \"\", \"task_name\": \"\", " +
                            "\"description\": \"\", \"time_started\": \"\", \"time_stopped\": " +
                            "\"\", \"date_received\": \"\", \"time_received\": \"\", " +
                            "\"total_time\": \"\"}]";
                }
            }
            else
            {
                output = "no tasks";
            }

            return output;
        }
    }
}