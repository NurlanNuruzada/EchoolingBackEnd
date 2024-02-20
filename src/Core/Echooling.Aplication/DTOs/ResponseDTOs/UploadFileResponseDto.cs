using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Aplication.DTOs.ResponseDTOs
{
    public record UploadFileResponseDto(string pathOrUrl,string fileName)
    {
    }
}
