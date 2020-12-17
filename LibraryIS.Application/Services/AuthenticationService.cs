using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Core.Entities;
using LibraryIS.Core.Interfaces;
using LibraryIS.CrossCutting.Constants;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using IAuthenticationService = LibraryIS.Application.Interfaces.IAuthenticationService;
using AuthenticationOptions = LibraryIS.CrossCutting.Configuration.AuthenticationOptions;

namespace LibraryIS.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AuthenticationOptions _authOptions;
        private readonly ILogger _logger;

        public AuthenticationService(IUnitOfWork unitOfWork, IOptions<AuthenticationOptions> authOptions, ILogger logger)
        {
            _unitOfWork = unitOfWork;
            _authOptions = authOptions.Value;
            _logger = logger;
        }

        public async Task<JwtTokenDto> Authenticate(LoginDto loginRequest)
        {
            var identity = await GetIdentity(loginRequest);
            if (identity == null)
            {
                _logger.Warning("User not found.");
                throw new UnauthorizedAccessException("Invalid username or password.");
            }
            var now = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwt = new JwtSecurityToken(
                _authOptions.Issuer,
                _authOptions.Audience,
                notBefore: now,
                claims: identity.Claims,
                expires: now.Add(TimeSpan.FromMinutes(15)),
                signingCredentials: credentials);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtTokenDto
            {
                AccessToken = encodedJwt,
                UserName = identity.Name,
            };
        }

        public async Task Register(RegisterDto registerRequest)
        {
            var userExists = _unitOfWork.GetRepository<User>().Exist(x =>
                x.ReaderProfile != null && (x.Email == registerRequest.Email || x.ReaderProfile.LibraryCard == registerRequest.LibraryCard ||
                                            x.ReaderProfile.PassportSeriesAndNumber == registerRequest.PassportAndSeriesNumber));
            if (userExists)
            {
                _logger.Warning("Error registering a new reader. The reader already exists.");
                throw new Exception("The user already exists. Check all the credentials you enter.");
            }

            var newUser = new User
            {
                Email = registerRequest.Email,
                ReaderProfile = new ReaderProfile
                {
                    PassportSeriesAndNumber = registerRequest.PassportAndSeriesNumber,
                    BornYear = registerRequest.BornYear,
                    LibraryCard = registerRequest.LibraryCard
                },
                Password = registerRequest.Password,
                FirstName = registerRequest.FirstName,
                CreationDate = DateTime.Now,
                IsApproved = true,
                LastName = registerRequest.LastName,
            };
            _unitOfWork.GetRepository<User>().Add(newUser);
            await _unitOfWork.Commit();
        }

        private async Task<ClaimsIdentity> GetIdentity(LoginDto login)
        {
            var user = await _unitOfWork.GetRepository<User>()
                .FindAsync(x => x.Email == login.Email && x.Password == login.Password);
            if (user != null)
            {
                var role = user.IsAdmin ? AuthConstants.AdminRoleName : AuthConstants.UserRoleName;
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
                };
                ClaimsIdentity claimsIdentity =
                    new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            return null;
        }
    }
}
