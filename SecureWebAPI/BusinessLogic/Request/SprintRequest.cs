using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Request
{
    public class SprintRequest : BaseRequest
    {
        public int Id { get; set; }
        public SprintVM Sprint { get; internal set; }
        public string UserId { get; internal set; }
        public int TeamId { get; set; }
    }
}