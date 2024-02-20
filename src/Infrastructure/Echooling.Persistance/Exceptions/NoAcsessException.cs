using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Persistance.Exceptions
{
    public class NoAcsessException : Exception, IBaseException
    {
        public int StatusCode { get; set; }

        public string CustomMessage { get; set; }
        public NoAcsessException(string message) : base(message)
        {
            StatusCode = (int)HttpStatusCode.Conflict;
            CustomMessage = message;
        }
    }
}
