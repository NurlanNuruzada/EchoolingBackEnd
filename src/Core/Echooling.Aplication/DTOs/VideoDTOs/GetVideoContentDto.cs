using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Echooling.Aplication.DTOs.VideoDTOs
{
    public class GetVideoContentDto
    {
        public Guid Guid { get; set; }
        public string? VideoTitle { get; set; }
        public string? VideoUniqueName { get; set; } = null;
    }
}
