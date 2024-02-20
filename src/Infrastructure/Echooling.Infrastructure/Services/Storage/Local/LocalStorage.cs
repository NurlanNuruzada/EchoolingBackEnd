using Echooling.Aplication.Abstraction.Storage.Local;
using Echooling.Aplication.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Http;

namespace Echooling.Infrastructure.Services.Storage.Local;
public class LocalStorage : ILocalService
{
    public void DeleteAsync(string fileName, string fileSource)
    {
        throw new NotImplementedException();
    }

    public bool HasFile(string fileName, string fileSource)
    {
        throw new NotImplementedException();
    }

    public Task<UploadFileResponseDto> UploadAsync(IFormFile file, string destination)
    {
        throw new NotImplementedException();
    }

    public Task<List<UploadFileResponseDto>> UploadAsync(IFormFileCollection file, string destination)
    {
        throw new NotImplementedException();
    }
}
