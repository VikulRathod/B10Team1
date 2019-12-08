using Store.DAL;
using Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.BLL
{
    public class AuthenticationBusiness
    {
        public User AuthenticateUser(User user)
        {
            AuthenticationData dal = new AuthenticationData();
            return dal.AuthenticateUser(user);
        }
        
        public bool IsPasswordResetLinkValid(string uid)
        {
            AuthenticationData dal = new AuthenticationData();
            return dal.IsPasswordResetLinkValid(uid);
        }

        public bool ChangeUserPassword(string uid, string newPwd)
        {
            AuthenticationData dal = new AuthenticationData();
            return dal.ChangeUserPassword(uid, newPwd);
        }
    }
}
