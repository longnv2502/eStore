using DataAccess.Dtos;
using DataAccess.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    public interface IAuthManager
    {
        String GetUserId();
        IEnumerable<Claim>? DecodeJwt2Claims();
        Task<bool> ValidateUser(LoginUserDTO userDTO);
        Task<string> CreateToken();
        Task<string> CreateRefreshToken();
        Task<TokenRequest> VerifyRefreshToken(TokenRequest request);
    }
}
