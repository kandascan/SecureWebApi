using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Model
{
    public class SprintTask
    {
        public int ColumnId { get; set; }
        public string ColumnName { get; set; }
        public int? OrderId { get; set; }
        public int? SprintId { get; set; }
        public int? TaskId { get; set; }
        public string TaskName { get; set; }
        public string UserName { get; set; }
        public int Effort { get;  set; }
        public DateTime CreatedDate { get;  set; }
        public int Id { get; set; }
    }
}
