using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.EmailDTOs
{
    public class SentEmailDto
    {
        public string To { get; set; } = null!;
        public string Subject { get; set; }
        public string body { get; set; }
    };
}
