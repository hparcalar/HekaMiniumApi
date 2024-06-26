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
     public class ProjectFieldServiceController : HekaControllerBase{
        public ProjectFieldServiceController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<ProjectFieldServiceModel> Get()
        {
            ProjectFieldServiceModel[] data = new ProjectFieldServiceModel[0];
            try
            {
                data = _context.ProjectFieldService.Select(d => new ProjectFieldServiceModel{
                    Id = d.Id,
                    DocumentNo = d.DocumentNo,
                    ProjectId = d.ProjectId,
                    ServiceDate = d.ServiceDate,
                    ServiceStatus = d.ServiceStatus,
                    ServiceStatusText = "",
                    ProjectName = d.Project != null ? d.Project.ProjectName : "",
                    ServiceUserId = d.ServiceUserId,
                    UserCode = d.SysUser != null ? d.SysUser.UserCode : "",
                    UserName = d.SysUser != null ? d.SysUser.UserName : "",
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("ByProject/{projectId}")]
        public IEnumerable<ProjectFieldServiceModel> GetByProject(int projectId)
        {
            ProjectFieldServiceModel[] data = new ProjectFieldServiceModel[0];
            try
            {
                data = _context.ProjectFieldService.Where(d => d.ProjectId == projectId).Select(d => new ProjectFieldServiceModel{
                    Id = d.Id,
                    DocumentNo = d.DocumentNo,
                    ProjectId = d.ProjectId,
                    ServiceDate = d.ServiceDate,
                    ServiceStatus = d.ServiceStatus,
                    ServiceStatusText = "",
                    ServiceUserId = d.ServiceUserId,
                    UserCode = d.SysUser != null ? d.SysUser.UserCode : "",
                    UserName = d.SysUser != null ? d.SysUser.UserName : "",
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public ProjectFieldServiceModel Get(int id)
        {
            ProjectFieldServiceModel data = new ProjectFieldServiceModel();
            try
            {
                data = _context.ProjectFieldService.Where(d => d.Id == id).Select(d => new ProjectFieldServiceModel{
                        Id = d.Id,
                        DocumentNo = d.DocumentNo,
                        ProjectId = d.ProjectId,
                        ServiceDate = d.ServiceDate,
                        ServiceStatus = d.ServiceStatus,
                        ServiceStatusText = "",
                        ServiceUserId = d.ServiceUserId,
                        UserCode = d.SysUser != null ? d.SysUser.UserCode : "",
                        UserName = d.SysUser != null ? d.SysUser.UserName : "",
                    }).FirstOrDefault();

                if (data != null){
                    data.Details = _context.ProjectFieldServiceDetail.Where(d => d.ProjectFieldServiceId == id)
                        .Select(d => new ProjectFieldServiceDetailModel{
                            Id = d.Id,
                            EndDate = d.EndDate,
                            LineNumber = d.LineNumber,
                            ProjectFieldServiceId = d.ProjectFieldServiceId,
                            ServiceStatus = d.ServiceStatus,
                            ServiceStatusText = "",
                            StartDate = d.StartDate,
                            WorkExplanation = d.WorkExplanation,
                        }).ToArray();

                    var attachments = _context.ProjectFieldServiceAttachment.Where(d => d.ProjectFieldServiceId == id)
                        .ToList()
                        .Select(d => new ProjectFieldServiceAttachmentModel{
                            Id = d.Id,
                            ContentBase64 = d.FileContent != null ? Convert.ToBase64String(d.FileContent) : "",
                            FileExtension = d.FileExtension,
                            FileHeader = d.FileHeader,
                            ProjectFieldServiceDetailId = d.ProjectFieldServiceDetailId,
                            ProjectFieldServiceId = d.ProjectFieldServiceId,
                        }).ToArray();

                    foreach (var item in attachments)
                    {
                        if (item.ProjectFieldServiceDetailId > 0){
                            var relatedDetail = data.Details.FirstOrDefault(d => d.Id == item.ProjectFieldServiceDetailId);
                            if (relatedDetail != null){
                                if (relatedDetail.Attachments == null)
                                    relatedDetail.Attachments = new ProjectFieldServiceAttachmentModel[0];

                                relatedDetail.Attachments = relatedDetail.Attachments.Concat(new ProjectFieldServiceAttachmentModel[]{ item }).ToArray();
                            }
                        }
                    }
                }
                else{
                    data = new ProjectFieldServiceModel{
                        Details = new ProjectFieldServiceDetailModel[0],
                    };
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(ProjectFieldServiceModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ProjectFieldService.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new ProjectFieldService();
                    _context.ProjectFieldService.Add(dbObj);
                    
                    dbObj.ServiceDate = DateTime.Now;
                }

                if (model.ServiceDate == null)
                    model.ServiceDate = DateTime.Now;

                // if (_context.ProjectFieldService.Any(d => d.Prof == model.WarehouseCode && d.PlantId == model.PlantId && d.Id != model.Id))
                //     throw new Exception("Bu depo koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

                model.MapTo(dbObj);

                #region SAVE DETAILS
                var currentDetails = _context.ProjectFieldServiceDetail.Where(d => d.ProjectFieldServiceId == dbObj.Id).ToArray();

                var removedDetails = currentDetails.Where(d => !model.Details.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedDetails)
                {
                    _context.ProjectFieldServiceDetail.Remove(item);
                }

                foreach (var item in model.Details)
                {
                    var dbDetail = _context.ProjectFieldServiceDetail.FirstOrDefault(d => d.Id == item.Id);
                    if (dbDetail == null){
                        dbDetail = new ProjectFieldServiceDetail();
                        _context.ProjectFieldServiceDetail.Add(dbDetail);
                    }

                    item.MapTo(dbDetail);
                    dbDetail.ProjectFieldService = dbObj;

                    if (item.Attachments != null && item.Attachments.Length > 0){
                        foreach (var itemAttach in item.Attachments)
                        {
                            var dbAttach = _context.ProjectFieldServiceAttachment.FirstOrDefault(d => d.Id == item.Id);
                            if (dbAttach == null){
                                dbAttach = new ProjectFieldServiceAttachment();
                                _context.ProjectFieldServiceAttachment.Add(dbAttach);
                            }

                            item.MapTo(dbDetail);
                            
                            if (!string.IsNullOrEmpty(itemAttach.ContentBase64)){
                                dbAttach.FileContent = Convert.FromBase64String(itemAttach.ContentBase64);
                            }

                            dbAttach.ProjectFieldService = dbObj;
                            dbAttach.ProjectFieldServiceDetail = dbDetail;
                        }
                    }
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
        [HttpDelete]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ProjectFieldService.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen kayıt bulunamadı.");

                var attachments = _context.ProjectFieldServiceAttachment.Where(d => d.ProjectFieldServiceId == id);
                foreach (var item in attachments)
                {
                    _context.ProjectFieldServiceAttachment.Remove(item);
                }

                var details = _context.ProjectFieldServiceDetail.Where(d => d.ProjectFieldServiceId == id).ToArray();
                foreach (var item in details)
                {
                    _context.ProjectFieldServiceDetail.Remove(item);
                }

                _context.ProjectFieldService.Remove(dbObj);

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