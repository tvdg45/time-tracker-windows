using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static Timothys_Digital_Solutions_Time_Tracker.views.Show_Change_Tasks;

namespace Timothys_Digital_Solutions_Time_Tracker.controllers
{
    public class Control_Create_Task : models.Change_Tasks
    {
        //global variables
        public static MySql.Data.MySqlClient.MySqlConnection use_connection;
        public static string admin_session;
        public static string task_name;
        public static string task_description;
        public static string time_started;
        public static string date_received;
        public static string time_received;
        public static string create_task;

        public static string Control_create_task()
        {
            string output = "";

            if (create_task.Equals("Create task"))
            {
                connection = use_connection;

                Set_admin_session(admin_session);
                Set_task_name(task_name);
                Set_task_description(task_description);
                Set_time_started(time_started);
                Set_date_received(date_received);
                Set_time_received(time_received);

                if (Create_task().Equals("success"))
                {
                    output = views.Show_Change_Tasks.Refresh_page();
                }
                else
                {
                    output = "";
                }

                try
                {
                    use_connection.Close();
                }
                catch (MySqlException)
                {
                }
            }

            return output;
        }
    }
}
