<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="time-tracker-interface.aspx.cs" Inherits="Timothys_Digital_Solutions_Time_Tracker.Time_Tracker_Interface" %>
<%
    MySql.Data.MySqlClient.MySqlConnection use_open_connection = Timothys_Digital_Solutions_Time_Tracker.configuration.Config.OpenConnection();

    string admin_session = Request.Form["admin_session"];
    string output = "";
    string print_web_page = "";

    if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(admin_session))
    {
        admin_session = "";
    }

    print_web_page += "<script type=\"text/javascript\" src=\"https://www.timothysdigitalsolutions.com/backstretch/js/jquery-3.2.1.js\"></script>\n";
    print_web_page += "<script type=\"text/javascript\" src=\"https://www.timothysdigitalsolutions.com/backstretch/js/jquery.min.js\"></script>\n";
    print_web_page += "<script type=\"text/javascript\" src=\"https://www.timothysdigitalsolutions.com/backstretch/js/jquery.backstretch.js\"></script>\n";
    print_web_page += "<div style=\"text-align: left; width: 99%; background-color: #FBDFCC; margin-left: 0.5%; margin-right: 0.5%; margin-top: auto; margin-bottom: auto; font-family: Arial, Helvetica, sans-serif;\">\n";
    print_web_page += "<div style=\"text-align: left; width: 100%\">\n";
    print_web_page += "<label>If page does not load, click <a href=\"https://tdscloud-dev-ed.lightning.force.com/lightning/page/home\">here</a> to refresh.</label><br /><br />\n";
    print_web_page += "</div>\n\n";

    if (!(admin_session.Equals("")))
    {
        Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Search_Tasks.task_owner = admin_session;
        Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Search_Tasks.use_connection = use_open_connection;

        output = Timothys_Digital_Solutions_Time_Tracker.controllers.Control_Search_Tasks.Request_tasks();

        if (Timothys_Digital_Solutions_Time_Tracker.utilities.Form_Validation.Is_string_null_or_whitespace(output))
        {
            output = "";
        }

        print_web_page += "<script type=\"text/javascript\">\n";
        print_web_page += "$(document).ready(function() {\n\n";
        print_web_page += "$(\"#task_list\").fadeIn();\n";
        print_web_page += "});\n\n";
        print_web_page += "function new_task() {\n\n";
        print_web_page += "$(\"#task_list\").fadeOut();\n";
        print_web_page += "$(\"#create_task_prompt\").fadeIn();\n";
        print_web_page += "}\n\n";
        print_web_page += "function view_tasks() {\n\n";
        print_web_page += "$(\"#create_task_prompt\").fadeOut();\n";
        print_web_page += "$(\"#task_list\").fadeIn();\n";
        print_web_page += "}\n\n";
        print_web_page += "function get_raw_minute() {\n\n";
        print_web_page += "var today_date = new Date();\n";
        print_web_page += "var raw_minute = parseInt((today_date.getTime() / 1000) / 60);\n\n";
        print_web_page += "return raw_minute;\n";
        print_web_page += "}\n\n";
        print_web_page += "function add_task() {\n\n";
        print_web_page += "var xhttp = new XMLHttpRequest();\n\n";
        print_web_page += "xhttp.onreadystatechange = function() {\n\n";
        print_web_page += "if (this.readyState == 4 && this.status == 200) {\n\n";
        print_web_page += "$(\"#create_task\").html(this.responseText);\n";
        print_web_page += "}\n";
        print_web_page += "};\n\n";
        print_web_page += "xhttp.open(\"POST\", \"" +
            Timothys_Digital_Solutions_Time_Tracker.configuration.Config.Third_party_domain() +
            "/create-task.aspx\");\n";
        print_web_page += "xhttp.setRequestHeader(\"Content-type\", \"application/x-www-form-urlencoded\");\n\n";
        print_web_page += "xhttp.send(\"task_name=\" + $(\"#task_name\").val() + \"&task_description=\" + $(\"#task_description\").val() + \"&time_started=\" + get_raw_minute() + \"&admin_session=" + admin_session + "&create_task=Create task\");\n";
        print_web_page += "}\n";
        print_web_page += "</script>\n";
        print_web_page += "<div id=\"create_task_prompt\" style=\"display: none;\">\n";
        print_web_page += "<div style=\"text-align: left; width: 100%\">\n";
        print_web_page += "<input type=\"button\" name=\"view_tasks\" id=\"view_tasks\" onclick=\"view_tasks()\" value=\"View tasks\" />\n";
        print_web_page += "</div>\n";
        print_web_page += "<div style=\"text-align: left; width: 100%\"><br />\n";
        print_web_page += "<label>Task name:</label>\n";
        print_web_page += "</div>\n";
        print_web_page += "<div style=\"text-align: left; width: 100%\">\n";
        print_web_page += "<input type=\"text\" id=\"task_name\" style=\"width: 98%\" />\n";
        print_web_page += "</div>\n";
        print_web_page += "<div style=\"text-align: left; width: 100%\"><br />\n";
        print_web_page += "<label>Task description:</label>\n";
        print_web_page += "</div>\n";
        print_web_page += "<div style=\"text-align: left; width: 100%\">\n";
        print_web_page += "<textarea id=\"task_description\" style=\"width: 98%; height: 100px\"></textarea>\n";
        print_web_page += "</div>\n";
        print_web_page += "<div style=\"text-align: left; width: 100%\"><br />\n";
        print_web_page += "<input type=\"button\" name=\"add_task\" id=\"add_task\" onclick=\"add_task()\" value=\"Add task\" />\n";
        print_web_page += "</div>\n";
        print_web_page += "<div style=\"text-align: left; width: 100%\"><br />\n";
        print_web_page += "<div id=\"create_task\"></div>\n";
        print_web_page += "</div>\n";
        print_web_page += "</div>\n";
        print_web_page += "<div id=\"task_list\" style=\"display: none;\">\n";
        print_web_page += "<div style=\"text-align: left; width: 100%\">\n";
        print_web_page += "<input type=\"button\" name=\"new_task\" id=\"new_task\" onclick=\"new_task()\" value=\"New task\" />\n";
        print_web_page += "</div>\n";

        if (output.Equals("no tasks"))
        {
            print_web_page += "<div style='text-align: left; padding-top: 20px; padding-bottom: 20px; word-wrap: break-word'>\n";
            print_web_page += "<label>You have no unsaved tasks.</label>\n";
            print_web_page += "</div>\n";
        }
        else
        {
            print_web_page += "<div style='text-align: left; padding-top: 20px; padding-bottom: 20px; word-wrap: break-word'>\n";
            print_web_page += "<div id='task_content'></div>\n";
            print_web_page += "</div>\n";
            print_web_page += "<script type=\"text/javascript\">\n\n";
            print_web_page += "var task_content = \"\";\n\n";
            print_web_page += "var each_task;\n\n";
            print_web_page += "var all_tasks;\n\n";
            print_web_page += "var number_of_selected_tasks = 0;\n\n";
            print_web_page += "var all_selected_tasks = \"\";\n\n";
            print_web_page += "var my_tasks = " + output + ";\n\n";
            print_web_page += "each_task = my_tasks;\n\n";
            print_web_page += "if (each_task.length > 0) {\n\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<br /><input type='button' name='delete_selected' id='delete_selected' onclick='erase_selected()' value='Delete selected' />\";\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "}\n\n";
            print_web_page += "if (each_task.length > 0) {\n\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<div id='finish_task'></div>\";\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<div id='delete_tasks'></div>\";\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "}\n\n";
            print_web_page += "for (var i = 0; i < each_task.length; i++) {\n\n";
            print_web_page += "if (each_task[i][\"user_id\"] == \"" + admin_session + "\") {\n\n";
            print_web_page += "task_content += \"<div style='text-align: left; padding-top: 10px; padding-bottom: 10px; word-wrap: break-word'>\";\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<input type='checkbox' name='select_task' class='select_task' value='\" + each_task[i][\"row_id\"] + \"' />\";\n";
            print_web_page += "task_content += \"<label><b>Name:</b> \" + each_task[i][\"task_name\"] + \"</label>\";\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<label><b>Description:</b> \" + each_task[i][\"description\"] + \"</label>\";\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<label><b>Task started: </b> \" + each_task[i][\"date_received\"] + \" at \" + each_task[i][\"time_received\"] + \"</label>\";\n";
            print_web_page += "task_content += \"</div>\";\n\n";
            print_web_page += "if (each_task[i][\"time_stopped\"] == \"\" || each_task[i][\"time_stopped\"].replace(/\\s/g, \"\").length == 0) {\n\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<input type='button' class='finish_task' value='Finish task' onclick='finish_task(\" + each_task[i]['row_id'] + \")' />\";\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "} else {\n\n";
            print_web_page += "if (each_task[i][\"total_time\"] == 1) {\n\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<label><b>Total time: </b> \" + each_task[i][\"total_time\"] + \" minute</label>\";\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "} else {\n\n";
            print_web_page += "task_content += \"<div style='text-align: left; width: 100%'>\";\n";
            print_web_page += "task_content += \"<label><b>Total time: </b> \" + each_task[i][\"total_time\"] + \" minutes</label>\";\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "}\n";
            print_web_page += "}\n\n";
            print_web_page += "task_content += \"</div>\";\n";
            print_web_page += "}\n";
            print_web_page += "}\n\n";
            print_web_page += "document.getElementById(\"task_content\").innerHTML = task_content;\n\n";
            print_web_page += "function finish_task(task_id) {\n\n";
            print_web_page += "var xhttp = new XMLHttpRequest();\n\n";
            print_web_page += "xhttp.onreadystatechange = function() {\n\n";
            print_web_page += "if (this.readyState == 4 && this.status == 200) {\n\n";
            print_web_page += "$(\"#finish_task\").html(this.responseText);\n";
            print_web_page += "}\n";
            print_web_page += "};\n\n";
            print_web_page += "xhttp.open(\"POST\", \"" +
                Timothys_Digital_Solutions_Time_Tracker.configuration.Config.Third_party_domain() +
                "/finish-selected-task.aspx\");\n";
            print_web_page += "xhttp.setRequestHeader(\"Content-type\", \"application/x-www-form-urlencoded\");\n\n";
            print_web_page += "xhttp.send(\"task_id=\" + task_id + \"&time_stopped=\" + get_raw_minute() + \"&admin_session=" + admin_session + "&finish_task=Finish task\");\n";
            print_web_page += "}\n\n";
            print_web_page += "function erase_selected() {\n\n";
            print_web_page += "all_tasks = document.getElementsByName('select_task').length;\n\n";
            print_web_page += "for (var i = 0; i < all_tasks; i++) {\n\n";
            print_web_page += "if (document.getElementsByName('select_task')[i].checked) {\n\n";
            print_web_page += "all_selected_tasks += document.getElementsByName('select_task')[i].value + \",\";\n";
            print_web_page += "number_of_selected_tasks++;\n";
            print_web_page += "}\n";
            print_web_page += "}\n\n";
            print_web_page += "all_selected_tasks += \"{}\";\n\n";
            print_web_page += "if (number_of_selected_tasks > 0) {\n\n";
            print_web_page += "all_selected_tasks = all_selected_tasks.replace(/,{}/g, \"\");\n";
            print_web_page += "} else {\n\n";
            print_web_page += "all_selected_tasks = all_selected_tasks.replace(/{}/g, \"\");\n";
            print_web_page += "}\n\n";
            print_web_page += "var xhttp = new XMLHttpRequest();\n\n";
            print_web_page += "xhttp.onreadystatechange = function() {\n\n";
            print_web_page += "if (this.readyState == 4 && this.status == 200) {\n\n";
            print_web_page += "$(\"#delete_tasks\").html(this.responseText);\n";
            print_web_page += "}\n";
            print_web_page += "};\n\n";
            print_web_page += "xhttp.open(\"POST\", \"" +
                Timothys_Digital_Solutions_Time_Tracker.configuration.Config.Third_party_domain() +
                "/delete-tasks.aspx\");\n";
            print_web_page += "xhttp.setRequestHeader(\"Content-type\", \"application/x-www-form-urlencoded\");\n";
            print_web_page += "xhttp.send(\"selected_tasks=\" + all_selected_tasks + \"&admin_session=" + admin_session + "&delete_tasks=Delete tasks\");\n\n";
            print_web_page += "number_of_selected_tasks = 0;\n";
            print_web_page += "all_selected_tasks = \"\";\n";
            print_web_page += "}\n";
            print_web_page += "</script>\n";
        }

        print_web_page += "</div>\n";
    }

    print_web_page += "</div>\n";

    Response.Write(print_web_page);
%>