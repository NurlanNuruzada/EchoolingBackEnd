using Echooling.Aplication.Abstraction.Storage;
using Echooling.Aplication.DTOs.ResponseDTOs;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Echooling.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public void DeleteAsync(string fileName, string fileSource)
         => _storage.DeleteAsync(fileName, fileSource); 

        public bool HasFile(string fileName, string fileSource)
        => _storage.HasFile(fileName, fileSource);  

        public Task<UploadFileResponseDto> UploadAsync(IFormFile file, string destination)
        =>_storage.UploadAsync(file, destination);  

        public Task<List<UploadFileResponseDto>> UploadAsync(IFormFileCollection file, string destination)
       => _storage.UploadAsync(file, destination);
    }
}
