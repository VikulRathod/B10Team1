using MyApp.BLL;
using MyApp.Models;
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

namespace _1_Authentication
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            #region Old Code to Call AuthenticateUser
            //// Authenticate againts the list stored in web.config
            ////if (FormsAuthentication.Authenticate(txtUserName.Text, txtPassword.Text))
            //if (AuthenticateUser(txtUserName.Text, txtPassword.Text))
            //{
            //    // Create the authentication cookie and redirect the user to welcome page
            //    FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkBoxRememberMe.Checked);
            //}
            //else
            //{
            //    lblMessage.Text = "Invalid UserName and/or password";
            //}
            #endregion Old Code to Call AuthenticateUser

            //AuthenticateUser(txtUserName.Text, txtPassword.Text);

            User user = new User()
            {
                UserName = txtUserName.Text,
                Password = new EncryptionAlgorithm().GetEncryptedValue(txtPassword.Text)
            };

            AuthenticateUserNew(user);
        }

        #region Old Implementation
        //private bool AuthenticateUser(string username, string password)
        //{
        //    // ConfigurationManager class is in System.Configuration namespace
        //    string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
        //    // SqlConnection is in System.Data.SqlClient namespace
        //    using (SqlConnection con = new SqlConnection(CS))
        //    {
        //        SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
        //        cmd.CommandType = CommandType.StoredProcedure;

        //        // FormsAuthentication is in System.Web.Security
        //        string EncryptedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
        //        // SqlParameter is in System.Data namespace
        //        SqlParameter paramUsername = new SqlParameter("@UserName", username);
        //        SqlParameter paramPassword = new SqlParameter("@Password", EncryptedPassword);

        //        cmd.Parameters.Add(paramUsername);
        //        cmd.Parameters.Add(paramPassword);

        //        con.Open();
        //        int ReturnCode = (int)cmd.ExecuteScalar();
        //        return ReturnCode == 1;
        //    }
        //}
        #endregion Old Implementation

        private void AuthenticateUserNew(User user)
        {
            AuthenticationBusiness bll = new AuthenticationBusiness();
            User userInfo = bll.AuthenticateUser(user);

            int RetryAttempts = userInfo.RetryAttempts;
            if (!userInfo.IsAuthenticated && !userInfo.IsAccountLocked && RetryAttempts == 0)
            {
                lblMessage.Text = "Not registered user. Please register first.";            
            }
            else if (userInfo.IsAccountLocked)
            {
                lblMessage.Text = "Account locked. Please contact administrator";
            }
            else if (RetryAttempts > 0)
            {
                int AttemptsLeft = (4 - RetryAttempts);
                lblMessage.Text = "Invalid user name and/or password. " +
                    AttemptsLeft.ToString() + "attempt(s) left";
            }
            else if (userInfo.IsAuthenticated)
            {
                Session["Username"] = txtUserName.Text;
                FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkBoxRememberMe.Checked);
            }
        }

        private void AuthenticateUser(string username, string password)
        {
            // ConfigurationManager class is in System.Configuration namespace
            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;
            // SqlConnection is in System.Data.SqlClient namespace
            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                //Formsauthentication is in system.web.security
                string encryptedpassword = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");

                //sqlparameter is in System.Data namespace
                SqlParameter paramUsername = new SqlParameter("@UserName", username);
                SqlParameter paramPassword = new SqlParameter("@Password", encryptedpassword);

                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    int RetryAttempts = Convert.ToInt32(rdr["RetryAttempts"]);
                    if (Convert.ToBoolean(rdr["AccountLocked"]))
                    {
                        lblMessage.Text = "Account locked. Please contact administrator";
                    }
                    else if (RetryAttempts > 0)
                    {
                        int AttemptsLeft = (4 - RetryAttempts);
                        lblMessage.Text = "Invalid user name and/or password. " +
                            AttemptsLeft.ToString() + "attempt(s) left";
                    }
                    else if (Convert.ToBoolean(rdr["Authenticated"]))
                    {
                        Session["Username"] = txtUserName.Text;
                        FormsAuthentication.RedirectFromLoginPage(txtUserName.Text, chkBoxRememberMe.Checked);
                    }
                }
            }
        }
    }
}