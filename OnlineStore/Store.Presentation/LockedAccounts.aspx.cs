using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Store.Presentation
{
    public partial class LockedAccounts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (User.Identity.Name.ToLower() == "test")
            {
                if (!IsPostBack)
                {
                    GetData();
                }
            }
            else
            {
                Response.Redirect("~/AccessDenied.aspx");
            }
        }

        private void GetData()
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spGetAllLocakedUserAccounts", con);
                cmd.CommandType = CommandType.StoredProcedure;

                con.Open();
                gvLockedAccounts.DataSource = cmd.ExecuteReader();
                gvLockedAccounts.DataBind();
            }
        }

        protected void gvLockedAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            EnableUserAccount(e.CommandArgument.ToString());
            GetData();
        }

        private void EnableUserAccount(string UserName)
        {
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spEnableUserAccount", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUserName = new SqlParameter()
                {
                    ParameterName = "@UserName",
                    Value = UserName
                };

                cmd.Parameters.Add(paramUserName);

                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}