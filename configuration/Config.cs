using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

//Add reference: MySql.Data
//Add reference: MySql.Web
//You might have to type the following command:
//Install-Package NLog.Config.
namespace Timothys_Digital_Solutions_Time_Tracker.configuration
{
    public class Config
    {
        private static readonly NLog.Logger LOGGER = NLog.LogManager.GetCurrentClassLogger();

        private static readonly string database_server = "0.0.0.0";
        private static readonly string port = "0000";
        private static readonly string database_username = "username";
        private static readonly string database_password = "password";
        private static readonly string database_name = "database";
        private static string connection_string;
        private static MySql.Data.MySqlClient.MySqlConnection use_connection;

        public static void Call_database_information()
        {
            connection_string = "server=" + database_server + ";port=" + port +
                ";uid=" + database_username + ";pwd=" + database_password +
                ";database=" + database_name;
        }

        public static MySqlConnection OpenConnection()
        {
            try
            {
                Call_database_information();

                use_connection = new MySql.Data.MySqlClient.MySqlConnection
                {
                    ConnectionString = connection_string
                };

                use_connection.Open();

                return use_connection;
            } catch (MySqlException)
            {
                LOGGER.Debug("Unable to connect to the database");

                return null;
            }
        }

        public static List<string> Bad_domain()
        {
            List<string> output = new List<string>
            {
                "timothysdigitalsolutions.com",
                "http://timothysdigitalsolutions.com",
                "http://www.timothysdigitalsolutions.com",
                "https://timothysdigitalsolutions.com"
            };

            return output;
        }

        public static string Domain()
        {
            string output = "";

            //Define any domain name below.  Your domain name can also have a directory included.
            //Example: Directory not included - https://www.timothysdigitalsolutions.com or directory included - https://www.timothysdigitalsolutions.com/contact-me
            string domain = "https://www.timothysdigitalsolutions.com";

            output += domain;

            return output;
        }

        public static string Third_party_domain()
        {
            string output = "";

            //Define any domain name below.  Your domain name can also have a directory included.

            //Example: Directory not included - https://www.timothysdigitalsolutions.com or directory included - https://www.timothysdigitalsolutions.com/contact-me
            //String third_party_domain = "https://user-account-management-1.herokuapp.com";
            string third_party_domain = "https://time-tracker-java.herokuapp.com";

            output += third_party_domain;

            return output;
        }
    }
}
