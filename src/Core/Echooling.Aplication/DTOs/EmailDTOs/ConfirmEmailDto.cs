using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.EmailDTOs
{
    public record ConfirmEmailDto(string userId , string token);
}
