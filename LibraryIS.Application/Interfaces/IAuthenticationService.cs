using System.Threading.Tasks;
using LibraryIS.Application.DTOs;

namespace LibraryIS.Application.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<JwtTokenDto> Authenticate(LoginDto loginRequest);
        public Task Register(RegisterDto registerRequest);
    }
}
