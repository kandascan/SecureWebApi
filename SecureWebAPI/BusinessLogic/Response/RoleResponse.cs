using SecureWebAPI.BusinessLogic.Model;
using System.Collections.Generic;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class RoleResponse : BaseResponse
    {
        public List<Role> Roles { get; set; }
    }
}
