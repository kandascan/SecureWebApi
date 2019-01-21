using SecureWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class TeamRequest : BaseRequest
    {
        public TeamVM Team { get; set; }
        public string UserId { get; internal set; }
        public int TeamId { get; internal set; }
    }
}