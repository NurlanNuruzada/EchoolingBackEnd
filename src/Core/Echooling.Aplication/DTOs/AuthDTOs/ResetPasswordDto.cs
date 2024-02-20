using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.AuthDTOs
{
    public class ResetPasswordDto
    {
        public string userId { get; set; } = null!;
        public string token { get; set; } = null!;
        public string password { get; set; } = null!;

    }
}
