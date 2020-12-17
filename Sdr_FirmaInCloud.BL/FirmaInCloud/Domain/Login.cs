using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDR_FirmaInCloud.BL
{
    public class Login
    {
        public int? KEY_LOG { get; set; }

        public string USER_ID { get; set; }

        public string PASSWORD_ID { get; set; }

        public string X_PERMISSION { get; set; }

        public string X_ADMIN { get; set; }

        public string X_TEMPLATE { get; set; }

        public string X_FIRST_LOG { get; set; }

        public DateTime DATE_EXPIRED { get; set; }

        public string NAME { get; set; }

        public string SURNAME { get; set; }
    }

    public class LoginAccess : Login 
    {
        public DateTime DT_LOGIN { get; set; }
    }
}
