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
using HekaMiniumApi.Models.Parameters;
using HekaMiniumApi.Helpers;

namespace HekaMiniumApi.Controllers{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [EnableCors()]
    public class StaffPermitController : HekaControllerBase{
        public StaffPermitController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<StaffPermitModel> Get()
        {
            StaffPermitModel[] data = new StaffPermitModel[0];
            try
            {
                data = _context.StaffPermit.Select(d => new StaffPermitModel{
                    Id = d.Id,
                    StaffId = d.StaffId,
                    StaffPermitExplanation = d.StaffPermitExplanation,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    UserCode = d.Staff != null ? d.Staff.UserCode : "",
                    UserName = d.Staff != null ? d.Staff.UserName : "",
                    PermitStatus = d.PermitStatus,
                    StatusText = d.PermitStatus == 0 ? "Onay bekleniyor" : 
                                    d.PermitStatus == 1 ? "Onaylandı" : "",
                    PermitType = d.PermitType,
                    PermitTypeText = d.PermitType == 1 ? "Mazeret İzni" :
                                    d.PermitType == 2 ? "Yıllık İzin" :
                                    d.PermitType == 3 ? "Raporlu İzin" : ""
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("byId/{id}")]
        public StaffPermitModel Get(int id)
        {
            StaffPermitModel data = new StaffPermitModel();
            try
            {
                data = _context.StaffPermit.Where(d => d.Id == id).Select(d => new StaffPermitModel{
                    Id = d.Id,
                    StaffId = d.StaffId,
                    StaffPermitExplanation = d.StaffPermitExplanation,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate,
                    UserName = d.Staff != null ? d.Staff.UserName : "",
                    PermitStatus = d.PermitStatus,
                    StatusText = d.PermitStatus == 0 ? "Onay bekleniyor" : 
                                    d.PermitStatus == 1 ? "Onaylandı" : "",
                    PermitType = d.PermitType,
                    PermitTypeText = d.PermitType == 1 ? "Mazeret İzni" :
                                    d.PermitType == 2 ? "Yıllık İzin" :
                                    d.PermitType == 3 ? "Raporlu İzin" : ""
                    }).FirstOrDefault();

                var dbEmp = _context.StaffPermit.Where(d => d.Id == id);
                var dbPermit = _context.StaffPermit.Where(d=> d.StaffId == dbEmp.FirstOrDefault().StaffId && d.StartDate.Value.Year == DateTime.Now.Year);
                var diffAnnual = 0;
                var diffReport = 0;
                var diffExcuse = 0;
                foreach (var item in dbPermit)
                {
                    if(item.PermitType == 1){
                        var day = (int)(item.EndDate.Value - item.StartDate.Value).TotalDays + 1;
                        diffExcuse = diffExcuse + day;
                    }
                    else if (item.PermitType == 2){
                        var day = (int)(item.EndDate.Value - item.StartDate.Value).TotalDays + 1;
                        diffAnnual = diffAnnual + day;
                    }
                    else{
                        var day = (int)(item.EndDate.Value - item.StartDate.Value).TotalDays + 1;
                        diffReport = diffReport + day;
                    }
                }
                data.AnnualPermitCount = diffAnnual;
                data.ReportPermitCount = diffReport;
                data.ExcusePermitCount = diffExcuse;
            }
            catch
            {
                
            }   
            return data;
        }

        [HttpGet]
        [Route("getTotal/{name}")]
        public StaffPermitModel GetTotal(string name)
        {
            StaffPermitModel data = new StaffPermitModel();
            try
            {
                var dbEmp = _context.StaffPermit.Where(d => d.Staff.UserName == name);
                var dbPermit = _context.StaffPermit.Where(d=> d.StaffId == dbEmp.FirstOrDefault().StaffId && d.StartDate.Value.Year == DateTime.Now.Year);
                var diffAnnual = 0;
                var diffReport = 0;
                var diffExcuse = 0;
                foreach (var item in dbPermit)
                {
                    if(item.PermitType == 1){
                        var day = (int)(item.EndDate.Value - item.StartDate.Value).TotalDays + 1;
                        diffExcuse = diffExcuse + day;
                    }
                    else if (item.PermitType == 2){
                        var day = (int)(item.EndDate.Value - item.StartDate.Value).TotalDays + 1;
                        diffAnnual = diffAnnual + day;
                    }
                    else{
                        var day = (int)(item.EndDate.Value - item.StartDate.Value).TotalDays + 1;
                        diffReport = diffReport + day;
                    }
                }
                data.AnnualPermitCount = diffAnnual;
                data.ReportPermitCount = diffReport;
                data.ExcusePermitCount = diffExcuse;
            }
            catch
            {
                
            }         
            return data;
        }

        [HttpGet]
        [Route("{staffId}")]
        [Authorize(Policy = "WebUser")]
        public IEnumerable<StaffPermitModel> DetailsOfPermit(int staffId){
            StaffPermitModel[] data = new StaffPermitModel[0];
            try
            {
                data = _context.StaffPermit.Where(d => d.StaffId == staffId).Select(d => new StaffPermitModel{
                    Id = d.Id,
                    StaffId = d.StaffId,
                    StaffPermitExplanation = d.StaffPermitExplanation,
                    StartDate = d.StartDate,
                    UserCode = d.Staff != null ? d.Staff.UserCode : "",
                    UserName = d.Staff != null ? d.Staff.UserName : "",
                    EndDate = d.EndDate,
                    PermitStatus = d.PermitStatus,
                    StatusText = d.PermitStatus == 0 ? "Onay bekleniyor" : 
                                d.PermitStatus == 1 ? "Onaylandı" : "",
                    PermitType = d.PermitType,
                    PermitTypeText = d.PermitType == 1 ? "Mazeret İzni" :
                                    d.PermitType == 2 ? "Yıllık İzin" :
                                    d.PermitType == 3 ? "Raporlu İzin" : ""
                })
                .OrderByDescending(d => d.Id)
                .ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }
        
        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(StaffPermitModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                int? userId = HekaHelpers.GetUserId(Request.HttpContext);
                if (userId != null)
                    model.StaffId = userId.Value;

                var dbObj = _context.StaffPermit.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new StaffPermit();
                    _context.StaffPermit.Add(dbObj);
                }

                //if (_context.StaffPermit.Any(d => d.StaffId == model.StaffId && d.StaffPermitExplanation == model.StaffPermitExplanation && d.Id != model.Id))
                //    throw new Exception("Bu personel için aynı açıklamayla izin isteği zaten mevcut. Farklı bir açıklama giriniz.");

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
        [HttpPost]
        [Route("ApprovePermit")]
        public BusinessResult ApprovePermit(PostIdModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.StaffPermit.FirstOrDefault(d => d.Id == model.Id);
                dbObj.PermitStatus = 1;

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
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.StaffPermit.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.StaffPermit.Remove(dbObj);

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