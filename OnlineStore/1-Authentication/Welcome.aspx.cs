using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1_Authentication
{
    public partial class Welcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //string uniqueId = Session["Username"] != null ? Session["Username"].ToString() : string.Empty;

            //string uniqueId = "";//rdr["UniqueId"].ToString();

            //string uId = new Guid().ToString();
            //Response.Redirect("~/Registration/ChangePasswordUsingCurrentPassword.aspx?uid=" + uId);
            //Response.Redirect("~/Registration/ChangePassword.aspx");

            //Response.Redirect("~/Registration/ResetPassword.aspx");
        }
    }
}