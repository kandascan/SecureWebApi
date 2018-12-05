using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class TeamEntity
    {
        public int TeamId { get; set; }
        public string TeamName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}