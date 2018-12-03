using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class BacklogEntity
    {
        public int BacklogId { get; set; }
        public int TeamId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}