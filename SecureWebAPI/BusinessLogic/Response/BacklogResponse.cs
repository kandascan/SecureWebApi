﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureWebAPI.Models;

namespace SecureWebAPI.BusinessLogic.Response
{
    public class BacklogResponse : BaseResponse
    {
        public List<TaskVM> Tasks { get; internal set; }
    }
}