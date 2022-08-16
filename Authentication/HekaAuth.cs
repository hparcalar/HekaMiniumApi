using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HekaMiniumApi.Authentication
{
    public class HekaAuth
    {
        private readonly string _key;
        public HekaAuth(string key)
        {
            this._key = key;
            this._key = "HekaMinium2021HekaButan2022 IS THE SCRET KEY OF THE COMPANY";
        }
        public string Authenticate(bool authenticated, string userName, int userId, HekaAuthType authType)
        {
            // create token handler
            var tokenHandler = new JwtSecurityTokenHandler();

            // set token key
            var tokenKey = System.Text.Encoding.ASCII.GetBytes(this._key);

            // set claim list
            List<Claim> claimList = new List<Claim>();

            // if (authType == MgAuthType.Dealer){
            //     claimList.Add(new Claim(ClaimTypes.Role, "FactoryOfficer"));
            //     claimList.Add(new Claim(ClaimTypes.Role, "Machine"));
            // }
            // else if (authType == MgAuthType.FactoryOfficer){
            //     claimList.Add(new Claim(ClaimTypes.Role, "Machine"));
            // }

            claimList.Add(new Claim(ClaimTypes.UserData, userId.ToString()));
            claimList.Add(new Claim(ClaimTypes.Name, userName));
            claimList.Add(new Claim(ClaimTypes.Role, authType.ToString()));

            // create token descriptor
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                claimList.ToArray()),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };

            // create token
            var token = tokenHandler.CreateToken(tokenDescriptor);

            // obtain token string
            var tokenStr = tokenHandler.WriteToken(token);

            return tokenStr;
        }
    }
}