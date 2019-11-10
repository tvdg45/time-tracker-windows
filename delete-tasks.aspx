<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="delete-tasks.aspx.cs" Inherits="Timothys_Digital_Solutions_Time_Tracker.Delete_Tasks" %>
<%
    MySql.Data.MySqlClient.MySqlConnection use_open_connection = Timothys_Digital_Solutions_Time_Tracker.configuration.Config.OpenConnection();

    string admin_session = Request.Form["admin_session"];
    string selected_tasks = Request.Form["selected_tasks"];
    string delete_tasks = Request.Form["delete_tasks"];
    string print_web_page = "";

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(admin_session))
    {
        admin_session = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(selected_tasks))
    {
        selected_tasks = "";
    }

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(delete_tasks))
    {
        delete_tasks = "";
    }

    if (delete_tasks.Equals("Delete tasks"))
    {
        if (!(admin_session.Equals("")))
        {
            string[] each_selected_task = Convert.ToString(selected_tasks).Split(',');

            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Delete_Tasks.use_connection = use_open_connection;
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Delete_Tasks.admin_session = Convert.ToString(admin_session);
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Delete_Tasks.each_selected_task = each_selected_task;
            Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Delete_Tasks.delete_tasks = Convert.ToString(delete_tasks);

            print_web_page += Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Delete_Tasks.Control_delete_tasks() + "\n";
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