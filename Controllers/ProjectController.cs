using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using HekaMiniumApi.Context;
using HekaMiniumApi.Models;
using HekaMiniumApi.Models.Operational;
using Microsoft.AspNetCore.Cors;
using HekaMiniumApi.Helpers;

namespace HekaMiniumApi.Controllers{
    
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class ProjectController : HekaControllerBase{
        public ProjectController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<ProjectModel> Get()
        {
            ProjectModel[] data = new ProjectModel[0];
            try
            {
                data = _context.Project.Select(d => new ProjectModel{
                    Id = d.Id,
                    Budget = d.Budget,
                    FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                    FirmId = d.FirmId,
                    FirmName = d.Firm != null ? d.Firm.FirmName : "",
                    Explanation = d.Explanation,
                    FirmLocation = d.FirmLocation,
                    PlantId = d.PlantId,
                    StartDate = d.StartDate,
                    DeadlineDate = d.DeadlineDate,
                    ProjectCategoryId = d.ProjectCategoryId,
                    ProjectCode = d.ProjectCode,
                    ProjectName = d.ProjectName,
                    ProjectPhaseTemplateId = d.ProjectPhaseTemplateId,
                    ResponsibleInfo = d.ResponsibleInfo,
                    ResponsiblePerson = d.ResponsiblePerson,
                    ProjectCategoryCode = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryCode : "",
                    ProjectCategoryName = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryName : "",
                    ProjectStatus = d.ProjectStatus,
                    ProjectStatusText = (d.ProjectStatus ?? 0) == 0 ? "Bekleniyor" :
                                        d.ProjectStatus == 1 ? "Çalışılıyor" :
                                        d.ProjectStatus == 2 ? "Tamamlandı" :
                                        d.ProjectStatus == 3 ? "İptal edildi" : ""
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Category")]
        public IEnumerable<ProjectCategoryModel> GetCategories()
        {
            ProjectCategoryModel[] data = new ProjectCategoryModel[0];
            try
            {
                data = _context.ProjectCategory.Select(d => new ProjectCategoryModel{
                    Id = d.Id,
                    IsActive = d.IsActive,
                    PlantId = d.PlantId,
                    ProjectCategoryCode = d.ProjectCategoryCode,
                    ProjectCategoryName = d.ProjectCategoryName,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("{id}")]
        public ProjectModel Get(int id)
        {
            ProjectModel data = new ProjectModel();
            try
            {
                data = _context.Project.Where(d => d.Id == id).Select(d => new ProjectModel{
                        Id = d.Id,
                        Budget = d.Budget,
                        FirmCode = d.Firm != null ? d.Firm.FirmCode : "",
                        FirmId = d.FirmId,
                        FirmName = d.Firm != null ? d.Firm.FirmName : "",
                        FirmLocation = d.FirmLocation,
                        PlantId = d.PlantId,
                        StartDate = d.StartDate,
                        DeadlineDate = d.DeadlineDate,
                        ProjectCategoryId = d.ProjectCategoryId,
                        ProjectCode = d.ProjectCode,
                        ProjectName = d.ProjectName,
                        ProjectPhaseTemplateId = d.ProjectPhaseTemplateId,
                        ResponsibleInfo = d.ResponsibleInfo,
                        ResponsiblePerson = d.ResponsiblePerson,
                        ProjectStatus = d.ProjectStatus,
                        ProjectStatusText = (d.ProjectStatus ?? 0) == 0 ? "Bekleniyor" :
                                            d.ProjectStatus == 1 ? "Çalışılıyor" :
                                            d.ProjectStatus == 2 ? "Tamamlandı" :
                                            d.ProjectStatus == 3 ? "İptal edildi" : ""
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Category/{id}")]
        public ProjectCategoryModel GetCategory(int id)
        {
            ProjectCategoryModel data = new ProjectCategoryModel();
            try
            {
                data = _context.ProjectCategory.Where(d => d.Id == id).Select(d => new ProjectCategoryModel{
                        Id = d.Id,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        ProjectCategoryCode = d.ProjectCategoryCode,
                        ProjectCategoryName = d.ProjectCategoryName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }


        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(ProjectModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Project.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Project();
                    _context.Project.Add(dbObj);
                }

                if (model.ProjectStatus == null)
                    model.ProjectStatus = 0;

                if (_context.Project.Any(d => d.ProjectCode == model.ProjectCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu proje koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

                model.MapTo(dbObj);

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
        [Route("Category")]
        [HttpPost]
        public BusinessResult PostCategory(ProjectCategoryModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ProjectCategory.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new ProjectCategory();
                    _context.ProjectCategory.Add(dbObj);
                }

                if (_context.ProjectCategory.Any(d => d.ProjectCategoryCode == model.ProjectCategoryCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu proje kategori koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

                model.MapTo(dbObj);

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
                var dbObj = _context.Project.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.Project.Remove(dbObj);

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

        [Authorize(Policy = "WebUser")]
        [Route("Category")]
        [HttpDelete]
        public BusinessResult DeleteCategory(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ProjectCategory.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.ProjectCategory.Remove(dbObj);

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

    }
}