<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="finish-selected-task.aspx.cs" Inherits="Timothys_Digital_Solutions_Time_Tracker.Finish_Selected_Task" %>
<%
    MySql.Data.MySqlClient.MySqlConnection use_open_connection = Timothys_Digital_Solutions_Time_Tracker.configuration.Config.OpenConnection();

    string admin_session = Request.Form["admin_session"];
    string task_id = Request.Form["task_id"];
    string time_stopped = Request.Form["time_stopped"];
    string finish_task = Request.Form["finish_task"];
    string print_web_page = "";

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(admin_session))
    {
        admin_session = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(task_id))
    {
        task_id = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(time_stopped))
    {
        time_stopped = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(finish_task))
    {
        finish_task = "";
    }

    if (finish_task.Equals("Finish task"))
    {
        if (!(admin_session.Equals("")))
        {
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Finish_Selected_Task.use_connection = use_open_connection;
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Finish_Selected_Task.admin_session = Convert.ToString(admin_session);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Finish_Selected_Task.task_id = Convert.ToString(task_id);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Finish_Selected_Task.time_stopped = Convert.ToString(time_stopped);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Finish_Selected_Task.finish_task = Convert.ToString(finish_task);

            print_web_page += Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Finish_Selected_Task.Control_finish_selected_task() + "\n";
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