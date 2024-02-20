using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ecooling.Domain.Entites
{
    public class VideoContent:BaseEntity
    {
        public string? VideoTitle { get; set; }
        public string? VideoUniqueName { get; set; } = null!;
        public Guid courseId { get; set; }
        public Course Course { get; set; } = null!;
    }
}
