using LibraryIS.Application.DTOs;
using System.Threading.Tasks;

namespace LibraryIS.Application.Interfaces
{
    public interface IAuthenticationService
    {
        public Task<JwtTokenDto> Authenticate(LoginDto loginRequest);
        public Task Register(RegisterDto registerRequest);
    }
}
