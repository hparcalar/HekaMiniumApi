using Microsoft.AspNetCore.Mvc;
using HekaMiniumApi.Context;
// using HekaMiniumApi.i18n;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using HekaMiniumApi.Authentication;

namespace HekaMiniumApi.Controllers{
    public class HekaControllerBase : ControllerBase{
        public HekaControllerBase(){
        }

        public HekaControllerBase(HekaMiniumSchema context){
            _context = context;
        }
        public HekaControllerBase(HekaMiniumSchema context, HekaAuth authObject){
            _context = context;
            _authObject = authObject;
        }

        public HekaControllerBase(HekaMiniumSchema context, IWebHostEnvironment environment){
            _context = context;
            _environment = environment;
        }
        
        
        protected HekaMiniumSchema _context;
        protected IWebHostEnvironment _environment;
        protected HekaAuth _authObject;
        protected string _userLanguage = "default";
        protected int _appUserId = 0;
        protected int _authType = 0;

        protected void ResolveHeaders(HttpRequest request){
            if (request != null && request.Headers != null && request.Headers.ContainsKey("Accept-Language"))
                _userLanguage = request.Headers["Accept-Language"];

            if (request != null)
                ResolveClaims(request.HttpContext);
        }

        private void ResolveClaims(HttpContext httpContext){
            if (httpContext.User != null && httpContext.User.Identity != null){
                var identity = httpContext.User.Identity as ClaimsIdentity;
                if (identity != null)
                {
                    IEnumerable<Claim> claims = identity.Claims; 
                    this._authType = claims.Any(d => d.Type == ClaimTypes.Role && d.Value == "SysAdmin") ? 1 :
                        claims.Any(d => d.Type == ClaimTypes.Role && d.Value == "WebUser") ? 2 : 
                        claims.Any(d => d.Type == ClaimTypes.Role && d.Value == "Operator") ? 3 : 
                        claims.Any(d => d.Type == ClaimTypes.Role && d.Value == "Device") ? 4 : 0;
                    
                    if (claims.Any(d => d.Type == ClaimTypes.UserData)){
                        this._appUserId = Convert.ToInt32(
                            claims.Where(d => d.Type == ClaimTypes.UserData)
                                .Select(d => d.Value).First()
                        );
                    }
                }
            }
        }
    }
}