using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace Timothys_Digital_Solutions_Time_Tracker.models
{
    public abstract class Search_Tasks
    {
        public static MySql.Data.MySqlClient.MySqlConnection connection;

        private static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        //global variables
        private static string task_owner;

        //mutators
        protected static void Set_task_owner(string this_task_owner)
        {
            task_owner = this_task_owner;
        }

        //accessors
        private static string Get_task_owner()
        {
            return task_owner;
        }

        private static void Create_new_table()
        {
            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "CREATE TABLE company_time_tracker_tasks " +
                    "(row_id INT NOT NULL, user_id TEXT NOT NULL, " +
                    "task_name TEXT NOT NULL, description TEXT NOT NULL, " +
                    "time_started TEXT NOT NULL, time_stopped TEXT NOT NULL, " +
                    "date_received TEXT NOT NULL, time_received TEXT NOT NULL, " +
                    "total_time TEXT NOT NULL, " +
                    "PRIMARY KEY (row_id)) ENGINE = MYISAM;";

                    command.ExecuteNonQuery();
                }
            } catch (MySqlException)
            {
                LOGGER.Debug("The 'company_time_tracker_tasks' " +
                    "table was not created because it already exists.  " +
                    "This is not necessarily an error.");
            }
        }

        protected static List<List<string>> Search_tasks()
        {
            List<List<string>> output = new List<List<string>>();

            List<string> row_id = new List<string>();
            List<string> user_id = new List<string>();
            List<string> task_name = new List<string>();
            List<string> description = new List<string>();
            List<string> time_started = new List<string>();
            List<string> time_stopped = new List<string>();
            List<string> date_received = new List<string>();
            List<string> time_received = new List<string>();
            List<string> total_time = new List<string>();

            int task_count = 0;

            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT row_id, user_id, task_name, description, " +
                        "time_started, time_stopped, date_received, time_received, total_time " +
                        "FROM company_time_tracker_tasks WHERE user_id = @prepare_user_id " +
                        "ORDER BY row_id DESC";

                    command.Parameters.AddWithValue("@prepare_user_id", Get_task_owner());

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            row_id.Add(reader.GetUInt64("row_id").ToString());
                            user_id.Add(reader.GetString("user_id"));
                            task_name.Add(reader.GetString("task_name"));
                            description.Add(reader.GetString("description"));
                            time_started.Add(reader.GetString("time_started"));
                            time_stopped.Add(reader.GetString("time_stopped"));
                            date_received.Add(reader.GetString("date_received"));
                            time_received.Add(reader.GetString("time_received"));
                            total_time.Add(reader.GetString("total_time"));

                            task_count++;
                        }
                    }
                }

                if (task_count == 0)
                {
                    row_id.Add("0");
                    user_id.Add("no task");
                    task_name.Add("no task");
                    description.Add("no task");
                    time_started.Add("no task");
                    time_stopped.Add("no task");
                    date_received.Add("no task");
                    time_received.Add("no task");
                    total_time.Add("no task");
                }
            } catch (MySqlException)
            {
                LOGGER.Debug("The 'company_time_tracker_tasks' " +
                    "table is corrupt or does not exist");

                Create_new_table();

                row_id.Add("0");
                user_id.Add("fail");
                task_name.Add("fail");
                description.Add("fail");
                time_started.Add("fail");
                time_stopped.Add("fail");
                date_received.Add("fail");
                time_received.Add("fail");
                total_time.Add("fail");
            }

            output.Add(row_id);
            output.Add(user_id);
            output.Add(task_name);
            output.Add(description);
            output.Add(time_started);
            output.Add(time_stopped);
            output.Add(date_received);
            output.Add(time_received);
            output.Add(total_time);

            return output;
        }
    }
}
