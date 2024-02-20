using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Persistance.Exceptions
{
    public interface IBaseException
    {
        int StatusCode { get; }
        string CustomMessage { get; }   
    }
}
