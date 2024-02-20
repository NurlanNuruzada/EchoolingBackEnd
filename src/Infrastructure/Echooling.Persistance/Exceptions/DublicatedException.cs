﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Echooling.Persistance.Exceptions
{
    public class DublicatedException:Exception, IBaseException
    {
        public int StatusCode { get; set; }

    public string CustomMessage { get; set; }
    public DublicatedException(string message) : base(message)
    {
        CustomMessage = message;
        StatusCode = (int)HttpStatusCode.Conflict;
    }
}
}
