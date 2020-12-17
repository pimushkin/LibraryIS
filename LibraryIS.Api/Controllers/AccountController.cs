using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LibraryIS.Application.DTOs;
using LibraryIS.Application.Interfaces;
using LibraryIS.Application.Services;
using LibraryIS.CrossCutting.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace LibraryIS.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Get a JWT token to access endpoints that require authorization.
        /// </summary>
        /// <param name="loginRequestDto">Credentials for authentication.</param>
        /// <returns></returns>
        [HttpPost("/token")]
        public async Task<ActionResult<JwtTokenDto>> Token(LoginDto loginRequestDto)
        {
            return await _authenticationService.Authenticate(loginRequestDto);
        }

        /// <summary>
        /// Registration of a new reader on the site.
        /// </summary>
        /// <param name="registerRequestDto">Credentials required for registration.</param>
        /// <returns></returns>
        [HttpPost("/register")]
        public async Task<ActionResult> Register(RegisterDto registerRequestDto)
        {
            await _authenticationService.Register(registerRequestDto);
            return Ok();
        }
    }
}
