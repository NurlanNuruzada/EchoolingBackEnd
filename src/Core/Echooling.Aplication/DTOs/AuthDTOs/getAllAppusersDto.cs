using System;
using System.Collections.Generic;

namespace Echooling.Aplication.DTOs.AuthDTOs
{
    public class GetAllAppUsersDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string? Email { get; set; }
        public List<string> Roles { get; set; }
    }
}