using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class GetEffortsResponse : BaseResponse
    {
        public List<EffortVM> Efforts { get; internal set; }
    }
}
