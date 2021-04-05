using CRCIS.Web.INoor.CRM.Contract.Authentication;
using CRCIS.Web.INoor.CRM.Contract.Settings;
using CRCIS.Web.INoor.CRM.Domain.Users.Admin;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CRCIS.Web.INoor.CRM.Infrastructure.Authentication
{
    public class JwtProvider : IJwtProvider
    {

        private readonly IJwtSettings _jwtSettings;

        public JwtProvider(IJwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }
        /// <summary>
        /// return First ValadateTo
        /// return second token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<(DateTime,string)> GenerateAsync(AdminModel user)
        {
            var secretKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey); // longer that 16 character
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature);

            var encryptionkey = Encoding.UTF8.GetBytes(_jwtSettings.EncryptKey); //must be 16 character
            var encryptingCredentials = new EncryptingCredentials(new SymmetricSecurityKey(encryptionkey), SecurityAlgorithms.Aes128KW, SecurityAlgorithms.Aes128CbcHmacSha256);

            var claims = _getClaims(user);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                IssuedAt = DateTime.Now,
                NotBefore = DateTime.Now.AddMinutes(_jwtSettings.NotBeforeMinutes),
                Expires = DateTime.Now.AddMinutes(_jwtSettings.ExpirationMinutes),
                SigningCredentials = signingCredentials,
                EncryptingCredentials = encryptingCredentials,
                Subject = new ClaimsIdentity(claims)
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return (token.ValidTo, tokenHandler.WriteToken(token));
        }
        private IEnumerable<Claim> _getClaims(AdminModel admin)
        {
            //add custom claims

            var list = new List<Claim>();
            list.Add(new Claim(ClaimTypes.SerialNumber,admin.SerialNumber));
            list.Add(new Claim(ClaimTypes.UserData, admin.Id.ToString()));
            list.Add(new Claim(ClaimTypes.Role, "Admin"));


            //var list = new List<Claim>
            //{
            //    new Claim(ClaimTypes.Name, user.UserName),
            //    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            //    //new Claim(ClaimTypes.MobilePhone, "09123456987"),
            //    //new Claim(securityStampClaimType, user.SecurityStamp.ToString())
            //};

            //var roles = new Role[] { new Role { Name = "Admin" } };
            //foreach (var role in roles)
            //    list.Add(new Claim(ClaimTypes.Role, role.Name));

            return list;
        }
    }
}
