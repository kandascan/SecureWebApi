using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class SprintColumnEntity
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
