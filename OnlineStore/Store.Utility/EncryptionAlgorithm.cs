using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;

namespace Store.Utility
{
    public class EncryptionAlgorithm
    {
        public string GetEncryptedValue(string val)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(val, "SHA1");
        }
    }
}
