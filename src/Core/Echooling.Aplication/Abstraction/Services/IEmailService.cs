using Echooling.Aplication.DTOs.EmailDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface IEmailService
    {
        void SendEmail(SentEmailDto sentEmailDto);
    }
}
