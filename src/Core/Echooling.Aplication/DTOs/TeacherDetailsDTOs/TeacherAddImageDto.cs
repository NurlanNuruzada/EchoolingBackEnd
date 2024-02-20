using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Echooling.Aplication.DTOs.TeacherDetailsDTOs
{
    public class TeacherAddImageDto
    {
        public IFormFile? image { get; set; } = null!;
    }
}
