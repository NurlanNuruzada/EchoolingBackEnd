using Echooling.Aplication.DTOs.EmailDTOs;

namespace Echooling.Aplication.Abstraction.Services
{
    public interface IConfirmEmail
    {
        Task ConfirmEmail(ConfirmEmailDto confirmEmailDto);
    }
}
