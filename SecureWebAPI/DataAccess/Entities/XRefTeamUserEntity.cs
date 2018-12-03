using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class XRefTeamUserEntity
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string UserId { get; set; }
        public int? RoleId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}