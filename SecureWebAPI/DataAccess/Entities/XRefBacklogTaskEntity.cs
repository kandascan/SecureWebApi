using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class XRefBacklogTaskEntity
    {
        public int Id { get; set; }
        public int BacklogId { get; set; }
        public int TaskId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}