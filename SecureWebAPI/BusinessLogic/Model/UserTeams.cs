using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecureWebAPI.BusinessLogic.Model
{
    public class UserTeams
    {
        public int TeamId { get; internal set; }
        public string TeamName { get; internal set; }
        public string UserRole { get; internal set; }
        public bool ScrumMasterUser { get; internal set; }
        public int TeamUserNumber { get; internal set; }
        public int TaskNumber { get; internal set; }
    }
}
