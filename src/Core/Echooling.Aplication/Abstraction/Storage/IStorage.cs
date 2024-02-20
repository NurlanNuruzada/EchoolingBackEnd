using Echooling.Aplication.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Http;

namespace Echooling.Aplication.Abstraction.Storage;
public interface IStorage
{
    Task<UploadFileResponseDto> UploadAsync(IFormFile file , string destination);
    Task<List<UploadFileResponseDto>> UploadAsync(IFormFileCollection file , string destination);
    void DeleteAsync(string fileName,string fileSource); 
    bool HasFile(string fileName,string fileSource);
}
