using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using static Timothys_Digital_Solutions_Time_Tracker.views.Show_Change_Tasks;

namespace Timothys_Digital_Solutions_Time_Tracker.controllers
{
    public class Control_Delete_Tasks : models.Change_Tasks
    {
        //global variables
        public static MySql.Data.MySqlClient.MySqlConnection use_connection;
        public static string admin_session;
        public static string[] each_selected_task;
        public static string delete_tasks;

        public static string Control_delete_tasks()
        {
            string output = "";

            if (delete_tasks.Equals("Delete tasks"))
            {
                if (each_selected_task.Length > 0 && !(each_selected_task[0].Equals("")))
                {
                    connection = use_connection;

                    Set_admin_session(admin_session);
                    Set_each_selected_task(each_selected_task);

                    if (Delete_selected_tasks().Equals("success"))
                    {
                        output = views.Show_Change_Tasks.Refresh_page();
                    } else
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
                else
                {
                    output = views.Show_Change_Tasks.Delete_tasks_error_message();
                }
            }

            return output;
        }
    }
}