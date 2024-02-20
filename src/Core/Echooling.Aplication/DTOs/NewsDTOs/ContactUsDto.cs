using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.NewsDTOs
{
    public class ContactUsDto
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string Subject { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
