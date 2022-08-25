using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using HekaMiniumApi.Context;
using HekaMiniumApi.Models;
using HekaMiniumApi.Models.Operational;
using Microsoft.AspNetCore.Cors;
using HekaMiniumApi.Authentication;
using HekaMiniumApi.Helpers;
using HekaMiniumApi.Models.Parameters;
using System.Text.Json;

namespace HekaMiniumApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class UserController : HekaControllerBase
    {
        public UserController(HekaMiniumSchema context, HekaAuth authObject) : base(context, authObject){ 
            ResolveHeaders(Request);
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("LoginSysUser")]
        public IActionResult LoginSysUser([FromBody] UserLoginModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                if (string.IsNullOrEmpty(model.PlantCode)){
                    throw new Exception("Any plant does not exist on the system.");
                }

                // create inital plant and web user
                if (!_context.SysUser.Any() && !_context.Plant.Any()){
                    var initPlant = new Plant{
                        PlantCode = "HekaPlant",
                        PlantName = "HekaPlant",
                        IsActive = true,
                    };
                    _context.Plant.Add(initPlant);

                    var rootRole = new SysRole{
                        IsActive = true,
                        IsRoot = true,
                        Plant = initPlant,
                        RoleAuthType = (int)HekaAuthType.SysAdmin,
                        RoleCode = "System Admin",
                        RoleName = "System Admin",
                    };
                    _context.SysRole.Add(rootRole);

                    var initUser = new SysUser{
                        UserCode = "SysAdmin",
                        DefaultLanguage = "tr",
                        IsActive = true,
                        UserName = "SysAdmin",
                        Password = HekaHelpers.ComputeSha256Hash("root"),
                        SysRole = rootRole,
                        Plant = initPlant,
                    };
                    _context.SysUser.Add(initUser);

                    _context.SaveChanges();

                    if (model.Login == "SysAdmin" && string.Equals("root", model.Password)){
                        result.Token = _authObject.Authenticate(true, "SysAdmin", initUser.Id, HekaAuthType.SysAdmin);
                        result.RecordId = initUser.Id;
                        result.PlantId = initUser.PlantId ?? 0;
                        result.InfoMessage = initUser.UserName;
                        result.Result = true;

                        result.AdditionalData = JsonSerializer.Serialize(new SysUserCredentials{
                            RoleAuthType = rootRole.RoleAuthType,
                            RoleName = rootRole.RoleName,
                            Sections = new SysRoleSectionModel[0],
                        });
                    }
                    else
                        throw new Exception("Invalid password.");
                }
                else{
                    var dbUser = _context.SysUser.FirstOrDefault(d => d.UserCode == model.Login && d.Plant != null && d.Plant.PlantCode == model.PlantCode);
                    if (dbUser == null)
                        throw new Exception("User information is invalid.");

                    if (!string.Equals(dbUser.Password, HekaHelpers.ComputeSha256Hash(model.Password)))
                        throw new Exception("Invalid password.");

                    result.Token = _authObject.Authenticate(true, dbUser.UserCode, dbUser.Id, HekaAuthType.WebUser);
                    result.RecordId = dbUser.Id;
                    result.PlantId = dbUser.PlantId ?? 0;
                    result.InfoMessage = dbUser.UserName;
                    result.Result = true;

                    var dbRole = _context.SysRole.FirstOrDefault(d => d.Id == dbUser.SysRoleId);

                    if (dbRole != null){
                        var sections = _context.SysRoleSection.Where(d => d.SysRoleId == dbRole.Id)
                            .Select(d => new SysRoleSectionModel{
                                Id = d.Id,
                                CanDelete = d.CanDelete,
                                CanRead = d.CanRead,
                                CanWrite = d.CanWrite,
                                SectionKey = d.SectionKey,
                                SysRoleId = d.SysRoleId,
                            }).ToArray();

                        result.AdditionalData = JsonSerializer.Serialize(new SysUserCredentials{
                            RoleAuthType = dbRole.RoleAuthType,
                            RoleName = dbRole.RoleName,
                            Sections = sections,
                        });
                    }
                }

                return Ok(JsonSerializer.Serialize(result));
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return Unauthorized(result);
        }

        [Authorize]
        [HttpGet]
        [Route("CheckToken")]
        public IActionResult CheckToken(){
            return Ok();
        }
    }
}
