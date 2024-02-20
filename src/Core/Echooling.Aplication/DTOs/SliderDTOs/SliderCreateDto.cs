using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Echooling.Aplication.DTOs.SliderDTOs
{
    public class SliderCreateDto
    {
        public string? Title { get; set; } = null!;
        public string? SeccondTile { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public IFormFile image { get; set; } = null!;
    }
}
