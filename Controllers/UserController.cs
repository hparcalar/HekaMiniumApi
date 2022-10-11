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

        [HttpGet]
        public IEnumerable<SysUserModel> Get()
        {
            SysUserModel[] data = new SysUserModel[0];
            try
            {
                data = _context.SysUser.Select(d => new SysUserModel{
                    Id = d.Id,
                    IsActive = d.IsActive,
                    PlantId = d.PlantId,
                    DefaultLanguage = d.DefaultLanguage,
                    Explanation = d.Explanation,
                    Password = "",
                    SysRoleId = d.SysRoleId,
                    UserCode = d.UserCode,
                    UserName = d.UserName,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public SysUserModel Get(int id)
        {
            SysUserModel data = new SysUserModel();
            try
            {
                data = _context.SysUser.Where(d => d.Id == id).Select(d => new SysUserModel{
                        Id = d.Id,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        DefaultLanguage = d.DefaultLanguage,
                        Explanation = d.Explanation,
                        Password = "",
                        SysRoleId = d.SysRoleId,
                        UserCode = d.UserCode,
                        UserName = d.UserName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(SysUserModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.SysUser.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new SysUser();
                    _context.SysUser.Add(dbObj);
                }

                if (_context.SysUser.Any(d => d.UserCode == model.UserCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu kullanıcı koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

                if (!string.IsNullOrEmpty(model.Password)){
                    model.Password = HekaHelpers.ComputeSha256Hash(model.Password);
                }

                // keep values
                var currentPass = dbObj.Password;
                model.MapTo(dbObj);

                if (string.IsNullOrEmpty(model.Password))
                    dbObj.Password = currentPass;

                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.Id;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [Authorize(Policy = "WebUser")]
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.SysUser.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.SysUser.Remove(dbObj);

                _context.SaveChanges();
                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #region ROLE MANAGEMENT
        [HttpGet]
        [Route("Role")]
        public IEnumerable<SysRoleModel> GetRoleList()
        {
            SysRoleModel[] data = new SysRoleModel[0];
            try
            {
                data = _context.SysRole.Select(d => new SysRoleModel{
                    Id = d.Id,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        IsRoot = d.IsRoot,
                        RoleAuthType = d.RoleAuthType,
                        RoleCode = d.RoleCode,
                        RoleName = d.RoleName,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Role/{id}")]
        public SysRoleModel GetRole(int id)
        {
            SysRoleModel data = new SysRoleModel();
            try
            {
                data = _context.SysRole.Where(d => d.Id == id).Select(d => new SysRoleModel{
                        Id = d.Id,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        IsRoot = d.IsRoot,
                        RoleAuthType = d.RoleAuthType,
                        RoleCode = d.RoleCode,
                        RoleName = d.RoleName,
                    }).FirstOrDefault();

                if (data != null && data.Id > 0){
                    data.Sections = _context.SysRoleSection.Where(d => d.SysRoleId == id)
                        .Select(d => new SysRoleSectionModel{
                            Id = d.Id,
                            CanDelete = d.CanDelete,
                            CanRead =d.CanRead,
                            CanWrite = d.CanWrite,
                            SectionKey = d.SectionKey,
                        }).ToArray();      
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "WebUser")]
        [Route("Role")]
        [HttpPost]
        public BusinessResult PostRole(SysRoleModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.SysRole.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new SysRole();
                    _context.SysRole.Add(dbObj);
                }

                if (_context.SysRole.Any(d => d.RoleCode == model.RoleCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu rol koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

                model.MapTo(dbObj);

                #region SAVE SECTIONS
                var currentSections = _context.SysRoleSection.Where(d => d.SysRoleId == dbObj.Id).ToArray();

                var removedSections = currentSections.Where(d => !model.Sections.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedSections)
                {
                    _context.SysRoleSection.Remove(item);
                }

                foreach (var item in model.Sections)
                {
                    var dbDetail = _context.SysRoleSection.FirstOrDefault(d => d.Id == item.Id);
                    if (dbDetail == null){
                        dbDetail = new SysRoleSection();
                        _context.SysRoleSection.Add(dbDetail);
                    }

                    item.MapTo(dbDetail);
                    dbDetail.SysRole = dbObj;
                }
                #endregion


                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.Id;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [Authorize(Policy = "WebUser")]
        [Route("Role")]
        [HttpDelete]
        public BusinessResult DeleteRole(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.SysRole.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.SysRole.Remove(dbObj);

                _context.SaveChanges();
                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        #endregion

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

                        var sections = _context.SysRoleSection.Where(d => d.SysRoleId == rootRole.Id)
                            .Select(d => new SysRoleSectionModel{
                                Id = d.Id,
                                CanDelete = d.CanDelete,
                                CanRead = d.CanRead,
                                CanWrite = d.CanWrite,
                                SectionKey = d.SectionKey,
                                SysRoleId = d.SysRoleId,
                            }).ToArray();

                        result.AdditionalData = JsonSerializer.Serialize(new SysUserCredentials{
                            RoleAuthType = rootRole.RoleAuthType,
                            RoleName = rootRole.RoleName,
                            Sections =  sections, //new SysRoleSectionModel[0],
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
    
        [Authorize(Policy = "WebUser")]
        [Route("ChangePass")]
        [HttpPost]
        public BusinessResult ChangePassword(UserChangePassModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.SysUser.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj != null){
                    dbObj.Password = HekaHelpers.ComputeSha256Hash(model.Password);
                    _context.SaveChanges();
                }

                result.Result=true;
                result.RecordId = dbObj.Id;
            }
            catch (System.Exception ex)
            {
                result.Result=false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

    }
}
