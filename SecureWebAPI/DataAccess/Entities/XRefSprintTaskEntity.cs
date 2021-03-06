﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.DataAccess.Entities
{
    public class XRefSprintTaskEntity
    {
        public int Id { get; set; }
        public int SprintId { get; set; }
        public int TaskId { get; set; }
        public int ColumnId { get; set; }
        public int OrderId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}