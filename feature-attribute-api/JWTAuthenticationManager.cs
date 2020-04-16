using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using feature_attribute_api.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace feature_attribute_api
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string apiKey);
    }

    public class JWTAuthenticationManager : IJWTAuthenticationManager
    {
        private readonly string _tokenKey;

        public JWTAuthenticationManager(IOptions<TokenSettings> optionsWrapper)
        {
            _tokenKey = optionsWrapper.Value.SecretKey;
        }

        public string Authenticate(string apiKey)
        {
            List<Features> availableFeatures;
            switch (apiKey.ToLower())
            {
                case "standard":
                    availableFeatures = new List<Features> { Features.GetForecast };
                    break;
                case "deluxe":
                    availableFeatures = new List<Features> { Features.GetForecast, Features.MakeItRain };
                    break;
                default:
                    return null;
            }

            // build our claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, apiKey),
                new Claim(ClaimTypes.NameIdentifier, apiKey),
                new Claim("features", JsonConvert.SerializeObject(availableFeatures)),
                new Claim("version", "1.0")
            };

            // build and return our JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}