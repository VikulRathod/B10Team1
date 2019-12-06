using MyApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.DAL
{
    public class AuthenticationData
    {
        public User AuthenticateUser(User user)
        {
            User userInfo = new User() { UserName = user.UserName, Password = user.Password };

            string CS = ConfigurationManager.ConnectionStrings["DBCS"].ConnectionString;

            using (SqlConnection con = new SqlConnection(CS))
            {
                SqlCommand cmd = new SqlCommand("spAuthenticateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paramUsername = new SqlParameter("@UserName", user.UserName);
                SqlParameter paramPassword = new SqlParameter("@Password", user.Password);

                cmd.Parameters.Add(paramUsername);
                cmd.Parameters.Add(paramPassword);

                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    userInfo.RetryAttempts = Convert.ToInt32(rdr["RetryAttempts"].ToString());
                    userInfo.IsAuthenticated = Convert.ToBoolean(rdr["Authenticated"]);
                    userInfo.IsAccountLocked = Convert.ToBoolean(rdr["AccountLocked"]);
                }

                return userInfo;
            }
        }

        public bool IsPasswordResetLinkValid(string uid)
        {
            List<SqlParameter> paramList = new List<SqlParameter>()
                {
                    new SqlParameter()
                    {
                        ParameterName = "@GUID",
                        Value = uid
                    }
                };

            return new ExecuteHelper().ExecuteSP("spIsPasswordResetLinkValid", paramList);
        }

        public bool ChangeUserPassword(string uid, string newPwd)
        {
            List<SqlParameter> paramList = new List<SqlParameter>()
            {
                new SqlParameter()
                {
                    ParameterName = "@GUID",
                    Value = uid
                },
                new SqlParameter()
                {
                    ParameterName = "@Password",
                    Value = newPwd
                }
            };
            return new ExecuteHelper().ExecuteSP("spChangePassword", paramList);
        }
    }
}
