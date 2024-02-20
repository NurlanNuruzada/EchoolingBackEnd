using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Persistance.Exceptions
{
    public class SignInFailureException : Exception, IBaseException
    {
        public int StatusCode {get; set;}   

        public string CustomMessage { get; set; }
        public SignInFailureException(string message):base(message)
        {
            CustomMessage = message;
            StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}
