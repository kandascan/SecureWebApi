using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureWebAPI.BusinessLogic.Model;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class TeamResponse : BaseResponse
    {
        public TeamVM Team { get; internal set; }
        public List<UserTeams> UserTeams { get; internal set; }
    }
}