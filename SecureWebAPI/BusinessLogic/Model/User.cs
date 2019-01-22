using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Model
{
    public class User
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserRole { get; internal set; }
        public bool Me { get; internal set; }
    }
}
