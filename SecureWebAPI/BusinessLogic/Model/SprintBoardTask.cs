using SecureWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Model
{
    public class SprintBoardTask
    {
        public string ColumnName { get; set; }
        public List<SprintTaskVM> Items { get; set; }
        public int ColumnId { get; set; }
    }
}
