﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SentezMicro.Web.Core.Results
{
  
        public interface IResult
        {
            public bool IsSuccess { get; set; }
            public string Message { get; set; }
        }
   
}
