using Crm.Domain.Customers;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Application.Authentication.DomainServices
{
    public class AuthenticationSecurity : IAuthenticateSecurity
    {
        private readonly string _jwtSecret;
        public AuthenticationSecurity(string jwtSecret)
        {
            _jwtSecret = jwtSecret;
        }
        public string CreateHashedSaltedPassword(string email, string password)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: Encoding.ASCII.GetBytes(email),
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8)); ;

            return hashed;
        }

        public KeyValuePair<string, long> GetAuthToken(Guid id)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, id.ToString())
                }),

                // Add expires time
                Expires = DateTime.UtcNow.AddHours(6),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var expToken = token.ValidTo.Ticks;
            var strToken = tokenHandler.WriteToken(token);

            return new KeyValuePair<string, long>(strToken, expToken);
        }


        public string GetApiToken(Guid customerId, IEnumerable<string> roles)
        {
            var claims = new List<Claim>();
            var baseClaimns = new[]
             {
                new Claim(JwtRegisteredClaimNames.Sub, customerId.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            // add roles
            claims.AddRange(baseClaimns);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecret));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: customerId.ToString(),
                audience: "hercle",
                expires: DateTime.UtcNow.AddYears(1),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetPublicId()
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numbers = "0123456789";
            var sLetters = new char[4];
            var sNumbers = new char[2];

            var random = new Random();

            for (int i = 0; i < sLetters.Length; i++)
            {
                sLetters[i] = letters[random.Next(letters.Length)];
            }

            for (int i = 0; i < sNumbers.Length; i++)
            {
                sNumbers[i] = numbers[random.Next(numbers.Length)];
            }

            return string.Concat(new string(sLetters), new string(sNumbers));
            
        }
    }
}
