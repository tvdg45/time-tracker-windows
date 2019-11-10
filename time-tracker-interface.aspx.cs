using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Http.Cors;

namespace Timothys_Digital_Solutions_Time_Tracker
{
    //When running this third party software on your server, it would be a good idea
    //to replace the * symbol to the name of an authorized website (example:
    //www.timothysdigitalsolutions.com).  Otherwise, XSS attacks could happen.
    [EnableCors(origins: "*", headers: "x-my-custom-header", methods: "post")]
    public partial class Time_Tracker_Interface : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}