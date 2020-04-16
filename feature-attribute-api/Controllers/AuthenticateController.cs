using System;
using feature_attribute_api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace feature_attribute_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private readonly ILogger<AuthenticateController> _logger;
        private readonly IJWTAuthenticationManager _authManager;

        public AuthenticateController(ILogger<AuthenticateController> logger, IJWTAuthenticationManager authenticationManager)
        {
            _logger = logger;
            _authManager = authenticationManager;
        }

        [AllowAnonymous]
        [HttpPost(Name = nameof(Authenticate))]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        public ActionResult<AccessToken> Authenticate([FromBody] TokenRequest apiKey)
        {
            var token = _authManager.Authenticate(apiKey.ApiKey);

            if (token == null)
                return Unauthorized();

            var jwt = new AccessToken
            {
                Token = token,
                Expires = DateTime.UtcNow.AddHours(1)
            };

            return Ok(jwt);
        }
    }
}