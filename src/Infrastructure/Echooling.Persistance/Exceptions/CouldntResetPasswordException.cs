using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Persistance.Exceptions
{
    public class CouldntResetPasswordException :Exception, IBaseException
    {
        public int StatusCode {get; set;}

        public string CustomMessage { get; set; }
        public CouldntResetPasswordException(string message):base(message)
        {
            CustomMessage = message;
            StatusCode = (int)HttpStatusCode.Conflict;
        }
    }
}
