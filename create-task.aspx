<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="create-task.aspx.cs" Inherits="Timothys_Digital_Solutions_Time_Tracker.Create_Task" %>
<%
    MySql.Data.MySqlClient.MySqlConnection use_open_connection = Timothys_Digital_Solutions_Time_Tracker.configuration.Config.OpenConnection();

    string admin_session = Request.Form["admin_session"];
    string task_name = Request.Form["task_name"];
    string task_description = Request.Form["task_description"];
    string time_started = Request.Form["time_started"];
    string date_received = DateTime.Now.ToString("yyyy-MM-dd");
    string time_received = DateTime.Now.ToString("hh:mm tt 'EST'");
    string create_task = Request.Form["create_task"];
    string print_web_page = "";

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(admin_session))
    {
        admin_session = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(task_name))
    {
        task_name = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(task_description))
    {
        task_description = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(time_started))
    {
        time_started = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(create_task))
    {
        create_task = "";
    }

    if (create_task.Equals("Create task"))
    {
        if (!(admin_session.Equals("")))
        {
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.use_connection = use_open_connection;
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.admin_session = Convert.ToString(admin_session);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.task_name = Convert.ToString(task_name);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.task_description = Convert.ToString(task_description);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.time_started = Convert.ToString(time_started);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.date_received = Convert.ToString(date_received);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.time_received = Convert.ToString(time_received);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.create_task = Convert.ToString(create_task);

            print_web_page += Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Create_Task.Control_create_task() + "\n";
        }
        else
        {
            print_web_page += "<script type=\"text/javascript\">\n";
            print_web_page += "window.location = document.location.href.replace(\"#\", \"\");\n";
            print_web_page += "</script>\n";
        }
    }

    Response.Write(print_web_page);
%>