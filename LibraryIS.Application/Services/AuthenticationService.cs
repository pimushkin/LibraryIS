﻿using LanguageExt;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Repositories;
using LibraryIS.CrossCutting.Constants;
using LibraryIS.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationOptions = LibraryIS.CrossCutting.Configuration.AuthenticationOptions;
using IAuthenticationService = LibraryIS.Application.Interfaces.IAuthenticationService;

namespace LibraryIS.Application.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository _userRepository;
        private readonly AuthenticationOptions _authOptions;
        private readonly ILogger _logger;

        public AuthenticationService(IUserRepository userRepository, IOptions<AuthenticationOptions> authOptions, ILogger logger)
        {
            _userRepository = userRepository;
            _authOptions = authOptions.Value;
            _logger = logger;
        }

        public async Task<JwtTokenDto> Authenticate(LoginDto loginRequest)
        {
            var identityOption = await GetIdentity(loginRequest);
            var identity = identityOption.Match(user => user, () =>
            {
                _logger.Warning("User not found.");
                throw new UnauthorizedAccessException("Invalid username or password.");
            });
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
                UserName = identity.Name ?? throw new InvalidOperationException(),
            };
        }

        public async Task Register(RegisterDto registerRequest)
        {
            var userExists = await _userRepository.CheckExistenceOfUserAsync(registerRequest.Email,
                registerRequest.LibraryCard, registerRequest.PassportAndSeriesNumber);
            if (userExists)
            {
                _logger.Warning("Error registering a new reader. The reader already exists.");
                throw new Exception("The user already exists. Check all the credentials you enter.");
            }

            var newUser = new User(registerRequest.FirstName, registerRequest.LastName, registerRequest.Email,
                registerRequest.Password, true, DateTime.Now, false,
                new ReaderProfile(registerRequest.LibraryCard, registerRequest.PassportAndSeriesNumber,
                    registerRequest.BornYear));
            await _userRepository.SaveAsync(newUser);
        }

        private async Task<Option<ClaimsIdentity>> GetIdentity(LoginDto login)
        {
            var userOption = await _userRepository.GetUserByCredentials(login.Email, login.Password);
            return userOption.Match(user =>
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
            }, () => Option<ClaimsIdentity>.None);
        }
    }
}
