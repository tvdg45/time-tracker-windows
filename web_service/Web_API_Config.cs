using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Timothys_Digital_Solutions_Time_Tracker.web_service
{
    public class Web_API_Config
    {
        public static void register(HttpConfiguration config)
        {
            config.EnableCors();

            config.Routes.MapHttpRoute(

                name: "Time_Tracker_API",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
