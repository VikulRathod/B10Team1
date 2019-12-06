using MyApp.BLL;
using MyApp.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace _1_Authentication.Registration
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                AuthenticationBusiness bll = new AuthenticationBusiness();
                if (!bll.IsPasswordResetLinkValid(Request.QueryString["uid"]))
                {
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                    lblMessage.Text = "Password Reset link has expired or is invalid";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string uid = Request.QueryString["uid"];
            string newPwd = new EncryptionAlgorithm().GetEncryptedValue(txtNewPassword.Text);

            AuthenticationBusiness bll = new AuthenticationBusiness();
            if (bll.ChangeUserPassword(uid, newPwd))
            {
                lblMessage.Text = "Password Changed Successfully!";
            }
            else
            {
                lblMessage.ForeColor = System.Drawing.Color.Red;
                lblMessage.Text = "Password Reset link has expired or is invalid";
            }
        }
    }
}