using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Linq;
using static Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation;

namespace Timothys_Digital_Solutions_Time_Tracker.models
{
    public abstract class Change_Tasks
    {
        public static MySql.Data.MySqlClient.MySqlConnection connection;

        private static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        //global variables
        private static string admin_session;
        private static string task_id;
        private static string[] each_selected_task;
        private static string task_name;
        private static string task_description;
        private static string time_started;
        private static string time_stopped;
        private static string date_received;
        private static string time_received;

        //mutators
        protected static void Set_admin_session(string this_admin_session)
        {
            admin_session = this_admin_session;
        }

        protected static void Set_task_id(string this_task_id)
        {
            task_id = this_task_id;
        }

        protected static void Set_each_selected_task(string[] this_each_selected_task)
        {
            each_selected_task = this_each_selected_task;
        }

        protected static void Set_task_name(string this_task_name)
        {
            task_name = this_task_name;
        }

        protected static void Set_task_description(string this_task_description)
        {
            task_description = this_task_description;
        }

        protected static void Set_time_started(string this_time_started)
        {
            time_started = this_time_started;
        }

        protected static void Set_time_stopped(string this_time_stopped)
        {
            time_stopped = this_time_stopped;
        }

        protected static void Set_date_received(string this_date_received)
        {
            date_received = this_date_received;
        }

        protected static void Set_time_received(string this_time_received)
        {
            time_received = this_time_received;
        }

        //accessors
        private static string Get_admin_session()
        {
            return admin_session;
        }

        private static string Get_task_id()
        {
            return task_id;
        }

        private static string[] Get_each_selected_task()
        {
            return each_selected_task;
        }

        private static string Get_task_name()
        {
            return task_name;
        }

        private static string Get_task_description()
        {
            return task_description;
        }

        private static string Get_time_started()
        {
            return time_started;
        }

        private static string Get_time_stopped()
        {
            return time_stopped;
        }

        private static string Get_date_received()
        {
            return date_received;
        }

        private static string Get_time_received()
        {
            return time_received;
        }

        private static int Generate_row_id()
        {
            int output = 0;

            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT row_id FROM " +
                        "company_time_tracker_tasks ORDER BY row_id DESC LIMIT 1";

                    if ((int)command.ExecuteScalar() > 0)
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                output = reader.GetInt32("row_id");
                            }
                        }
                    } else
                    {
                        output = 0;
                    }
                }
            }
            catch (Exception e)
            {
                LOGGER.Debug(e);

                output = 0;
            }

            return output + 1;
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
            }
            catch (MySqlException)
            {
                LOGGER.Debug("The 'company_time_tracker_tasks' " +
                    "table was not created because it already exists.  " +
                    "This is not necessarily an error.");
            }
        }

        private static int Calculate_total_minutes(int start_time, int finish_time)
        {
            int output;

            try
            {
                output = finish_time - start_time;
            }
            catch (Exception)
            {
                output = 0;
            }

            return output;
        }

        private static int Search_start_time(int row_id)
        {
            int output = 0;
            int task_count = 0;

            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT time_started FROM " +
                        "company_time_tracker_tasks WHERE row_id = @prepare_row_id";

                    command.Parameters.AddWithValue("@prepare_row_id", row_id);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            output = Convert.ToInt32(reader.GetString("time_started"));

                            task_count++;
                        }
                    }
                }

                if (task_count != 1)
                {
                    output = 0;
                }
            }
            catch (MySqlException)
            {
                LOGGER.Debug("The 'company_time_tracker_tasks' " +
                    "table is corrupt or does not exist");

                Create_new_table();

                output = 0;
            }

            return output;
        }

        protected static string Create_task()
        {
            string output;

            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO " +
                    "company_time_tracker_tasks (row_id, user_id, task_name, description, " +
                    "time_started, time_stopped, date_received, time_received, total_time) " +
                    "VALUES(@prepare_row_id, @prepare_user_id, @prepare_task_name, " +
                    "@prepare_description, @prepare_time_started, @prepare_time_stopped, " +
                    "@prepare_date_received, @prepare_time_received, @prepare_total_time)";

                    command.Parameters.AddWithValue("@prepare_row_id", Generate_row_id());
                    command.Parameters.AddWithValue("@prepare_user_id", Get_admin_session());

                    if (utilities.Form_Validation.Is_string_null_or_whitespace(Get_task_name()))
                    {
                        command.Parameters.AddWithValue("@prepare_task_name", "Unspecified task");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@prepare_task_name", Get_task_name());
                    }

                    if (utilities.Form_Validation.Is_string_null_or_whitespace(Get_task_description()))
                    {
                        command.Parameters.AddWithValue("@prepare_description", "None");
                    }
                    else
                    {
                        command.Parameters.AddWithValue("@prepare_description", Get_task_description());
                    }

                    command.Parameters.AddWithValue("@prepare_time_started", Get_time_started());
                    command.Parameters.AddWithValue("@prepare_time_stopped", "");
                    command.Parameters.AddWithValue("@prepare_date_received", Get_date_received());
                    command.Parameters.AddWithValue("@prepare_time_received", Get_time_received());
                    command.Parameters.AddWithValue("@prepare_total_time", "0");

                    command.ExecuteNonQuery();
                }

                output = "success";
            }
            catch (MySqlException)
            {
                LOGGER.Debug("The 'company_time_tracker_tasks' " +
                    "table is corrupt or does not exist");

                Create_new_table();

                output = "fail";
            }

            return output;
        }

        protected static string Finish_selected_task()
        {
            string output;

            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE company_time_tracker_tasks " +
                    "SET time_stopped = @prepare_time_stopped, " +
                    "total_time = @prepare_total_time " +
                    "WHERE user_id = @prepare_user_id AND row_id = @prepare_row_id";

                    command.Parameters.AddWithValue("@prepare_time_stopped", Get_time_stopped());
                    command.Parameters.AddWithValue("@prepare_total_time", Convert.ToString(Calculate_total_minutes(
                        Search_start_time(Convert.ToInt32(Get_task_id())),
                        Convert.ToInt32(Get_time_stopped()))
                        ));
                    command.Parameters.AddWithValue("@prepare_user_id", Get_admin_session());
                    command.Parameters.AddWithValue("@prepare_row_id", Convert.ToInt32(Get_task_id()));

                    command.ExecuteNonQuery();
                }

                output = "success";
            }
            catch (MySqlException)
            {
                LOGGER.Debug("The 'company_time_tracker_tasks' " +
                    "table is corrupt or does not exist");

                Create_new_table();

                output = "fail";
            }

            return output;
        }

        protected static string Delete_selected_tasks()
        {
            string output;
            string query_string = "";
            int records_to_delete = 0;
            int number_of_selected_tasks;

            try
            {
                using (MySqlCommand command = connection.CreateCommand())
                {
                    number_of_selected_tasks = Get_each_selected_task().Length;

                    for (int i = 0; i < number_of_selected_tasks; i++)
                    {
                        query_string += "DELETE FROM company_time_tracker_tasks " +
                        "WHERE user_id = @prepare_user_id_" + i +
                        " AND row_id = @prepare_row_id_" + i + ";";
                    }

                    command.CommandText = query_string;

                    for (int i = 0; i < number_of_selected_tasks; i++)
                    {
                        command.Parameters.AddWithValue("@prepare_user_id_" + i, Get_admin_session());
                        command.Parameters.AddWithValue("@prepare_row_id_" + i, Convert.ToInt32(Get_each_selected_task()[i]));

                        records_to_delete++;
                    }

                    if (records_to_delete > 0)
                    {
                        command.ExecuteNonQuery();
                    }
                }

                output = "success";
            }
            catch (MySqlException)
            {
                LOGGER.Debug("The 'company_time_tracker_tasks' " +
                    "table is corrupt or does not exist");

                Create_new_table();

                output = "fail";
            }

            return output;
        }
    }
}
