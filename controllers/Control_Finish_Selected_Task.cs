using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static Timothys_Digital_Solutions_Time_Tracker.views.Show_Change_Tasks;

namespace Timothys_Digital_Solutions_Time_Tracker.controllers
{
    public class Control_Finish_Selected_Task : models.Change_Tasks
    {
        //global variables
        public static MySql.Data.MySqlClient.MySqlConnection use_connection;
        public static string admin_session;
        public static string task_id;
        public static string time_stopped;
        public static string finish_task;

        public static string Control_finish_selected_task()
        {
            string output = "";

            if (finish_task.Equals("Finish task"))
            {
                connection = use_connection;

                Set_admin_session(admin_session);
                Set_task_id(task_id);
                Set_time_stopped(time_stopped);

                if (Finish_selected_task().Equals("success"))
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