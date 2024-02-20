namespace Echooling.Aplication.DTOs.ResponseDTOs
{
    public record TokenResponseDto(string token,
                                   DateTime expireDate,
                                   DateTime RefreshTokenExpiration,
                                   string RefreshToken,
                                   string UserName,
                                   string Fullname,
                                   string email) { }
}
