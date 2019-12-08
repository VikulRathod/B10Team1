using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RetryAttempts { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsAccountLocked { get; set; }
    }
}
