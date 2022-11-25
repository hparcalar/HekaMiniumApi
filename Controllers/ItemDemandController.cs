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
     public class ItemDemandController : HekaControllerBase{
        public ItemDemandController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<ItemDemandModel> Get()
        {
            ItemDemandModel[] data = new ItemDemandModel[0];
            try
            {
                data = _context.ItemDemand.Select(d => new ItemDemandModel{
                    Id = d.Id,
                    DeadlineDate = d.DeadlineDate,
                    DemandStatus = d.DemandStatus,
                    Explanation = d.Explanation,
                    IsOrdered = d.IsOrdered,
                    PlantId = d.PlantId,
                    ProjectId = d.ProjectId,
                    SysUserId = d.SysUserId,
                    UserCode = d.SysUser != null ? d.SysUser.UserCode : "",
                    UserName = d.SysUser != null ? d.SysUser.UserName : "",
                    ReceiptDate = d.ReceiptDate,
                    ReceiptNo = d.ReceiptNo,
                    IsContracted = d.IsContracted,
                    LastUpdateDate = d.LastUpdateDate,
                    ProjectCode = d.Project != null ? d.Project.ProjectCode : "",
                    ProjectName = d.Project != null ? d.Project.ProjectName : "",
                    StatusText = d.DemandStatus == 0 ? "Onay bekleniyor" : 
                                    d.DemandStatus == 1 ? "Onaylandı" :
                                    d.DemandStatus == 2 ? "Sipariş oluşturuldu" :
                                    d.DemandStatus == 3 ? "Sipariş teslim alındı" :
                                    d.DemandStatus == 4 ? "İptal edildi" : 
                                    d.DemandStatus == 5 ? "Teklif bekleniyor" : 
                                    d.DemandStatus == 6 ? "Sipariş iletildi" :
                                    d.DemandStatus == 7 ? "Kısmi teslim alındı" : "",
                })
                .OrderByDescending(d => d.ReceiptDate) 
                .ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize(Policy = "WebUser")]
        public ItemDemandModel Get(int id)
        {
            ItemDemandModel data = new ItemDemandModel();
            try
            {
                data = _context.ItemDemand.Where(d => d.Id == id).Select(d => new ItemDemandModel{
                        Id = d.Id,
                        DeadlineDate = d.DeadlineDate,
                        DemandStatus = d.DemandStatus,
                        Explanation = d.Explanation,
                        IsOrdered = d.IsOrdered,
                        PlantId = d.PlantId,
                        ProjectId = d.ProjectId,
                        ReceiptDate = d.ReceiptDate,
                        SysUserId = d.SysUserId,
                        ReceiptNo = d.ReceiptNo,
                        IsContracted = d.IsContracted,
                        LastUpdateDate = d.LastUpdateDate,
                        ProjectCode = d.Project != null ? d.Project.ProjectCode : "",
                        ProjectName = d.Project != null ? d.Project.ProjectName : "",
                        StatusText = d.DemandStatus == 0 ? "Onay bekleniyor" : 
                                    d.DemandStatus == 1 ? "Onaylandı" :
                                    d.DemandStatus == 2 ? "Sipariş oluşturuldu" :
                                    d.DemandStatus == 3 ? "Sipariş teslim alındı" :
                                    d.DemandStatus == 4 ? "İptal edildi" : 
                                    d.DemandStatus == 5 ? "Teklif bekleniyor" : 
                                    d.DemandStatus == 6 ? "Sipariş iletildi" :
                                    d.DemandStatus == 7 ? "Kısmi teslim alındı" : "",
                    }).FirstOrDefault();

                if (data != null && data.Id > 0){
                    data.Details = _context.ItemDemandDetail.Where(d => d.ItemDemandId == data.Id)
                        .Select(d => new ItemDemandDetailModel{
                            Id = d.Id,
                            DemandStatus = d.DemandStatus,
                            Explanation = d.Explanation,
                            ItemDemandId = d.ItemDemandId,
                            ItemExplanation = d.ItemExplanation,
                            ItemId = d.ItemId,
                            LineNumber = d.LineNumber,
                            NetQuantity = d.NetQuantity,
                            PartDimensions = d.PartDimensions,
                            PartNo = d.PartNo,
                            Quantity = d.Quantity,
                            UnitId = d.UnitId,
                            IsContracted = d.IsContracted,
                            ItemCode = d.Item != null ? d.Item.ItemCode : d.ItemExplanation,
                            ItemName = d.Item != null ? d.Item.ItemName : d.ItemExplanation,
                            ItemDemandNo = d.ItemDemand.ReceiptNo,
                            UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                            UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                            StatusText = d.DemandStatus == 0 ? "Onay bekleniyor" : 
                                    d.DemandStatus == 1 ? "Onaylandı" :
                                    d.DemandStatus == 2 ? "Sipariş oluşturuldu" :
                                    d.DemandStatus == 3 ? "Sipariş teslim alındı" :
                                    d.DemandStatus == 4 ? "İptal edildi" : 
                                    d.DemandStatus == 5 ? "Teklif bekleniyor" : 
                                    d.DemandStatus == 6 ? "Sipariş iletildi" :
                                    d.DemandStatus == 7 ? "Kısmi teslim alındı" : "",
                        }).ToArray();
                }
                else{
                    if (data == null)
                        data = new ItemDemandModel();

                    data.ReceiptNo = GetNextDemandNumber();
                    data.Details = new ItemDemandDetailModel[0];
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        private string GetNextDemandNumber(){
            try
            {
                int nextNumber = 1;
                var lastRecord = _context.ItemDemand.OrderByDescending(d => d.ReceiptNo).Select(d => d.ReceiptNo).FirstOrDefault();
                if (lastRecord != null && !string.IsNullOrEmpty(lastRecord))
                    nextNumber = Convert.ToInt32(lastRecord) + 1;

                return string.Format("{0:000000}", nextNumber);
            }
            catch (System.Exception)
            {
                
            }

            return string.Empty;
        }

        [HttpGet]
        [Route("OfProject/{projectId}")]
        [Authorize(Policy = "WebUser")]
        public IEnumerable<ItemDemandDetailModel> DetailsOfProject(int projectId){
            ItemDemandDetailModel[] data = new ItemDemandDetailModel[0];
            try
            {
                data = _context.ItemDemandDetail.Where(d => d.ItemDemand.ProjectId == projectId).Select(d => new ItemDemandDetailModel{
                    Id = d.Id,
                    DemandStatus = d.DemandStatus,
                    Explanation = d.Explanation,
                    ItemDemandId = d.ItemDemandId,
                    ItemExplanation = d.ItemExplanation,
                    ItemId = d.ItemId,
                    LineNumber = d.LineNumber,
                    NetQuantity = d.NetQuantity,
                    Quantity = d.Quantity,
                    UnitId = d.UnitId,
                    PartDimensions = d.PartDimensions,
                    PartNo = d.PartNo,
                    IsContracted = d.IsContracted,
                    ItemCode = d.Item != null ? d.Item.ItemCode : d.ItemExplanation,
                    ItemName = d.Item != null ? d.Item.ItemName : d.ItemExplanation,
                    UserCode = d.ItemDemand.SysUser != null ? d.ItemDemand.SysUser.UserCode : "",
                    UserName = d.ItemDemand.SysUser != null ? d.ItemDemand.SysUser.UserName : "",
                    ItemDemandNo = d.ItemDemand.ReceiptNo,
                    UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                    UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                    StatusText = d.DemandStatus == 0 ? "Onay bekleniyor" : 
                                    d.DemandStatus == 1 ? "Onaylandı" :
                                    d.DemandStatus == 2 ? "Sipariş oluşturuldu" :
                                    d.DemandStatus == 3 ? "Sipariş teslim alındı" :
                                    d.DemandStatus == 4 ? "İptal edildi" : 
                                    d.DemandStatus == 5 ? "Teklif bekleniyor" : 
                                    d.DemandStatus == 6 ? "Sipariş iletildi" :
                                    d.DemandStatus == 7 ? "Kısmi teslim alındı" : "",
                    DemandDate = d.ItemDemand.ReceiptDate,
                    DeadlineDate = d.ItemDemand.DeadlineDate,
                })
                .OrderByDescending(d => d.DemandDate)
                .ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("WaitingForApprove")]
        [Authorize(Policy = "WebUser")]
        public IEnumerable<ItemDemandDetailModel> GetWaitingForApprove(){
            ItemDemandDetailModel[] data = new ItemDemandDetailModel[0];
            try
            {
                data = _context.ItemDemandDetail.Where(d => (d.DemandStatus ?? 0) == 0 || d.DemandStatus == 4).Select(d => new ItemDemandDetailModel{
                    Id = d.Id,
                    DemandStatus = d.DemandStatus,
                    Explanation = d.Explanation,
                    ItemDemandId = d.ItemDemandId,
                    ItemExplanation = d.ItemExplanation,
                    ItemId = d.ItemId,
                    LineNumber = d.LineNumber,
                    NetQuantity = d.NetQuantity,
                    Quantity = d.Quantity,
                    UnitId = d.UnitId,
                    PartDimensions = d.PartDimensions,
                    PartNo = d.PartNo,
                    IsContracted = d.IsContracted,
                    UserCode = d.ItemDemand.SysUser != null ? d.ItemDemand.SysUser.UserCode : "",
                    UserName = d.ItemDemand.SysUser != null ? d.ItemDemand.SysUser.UserName : "",
                    ProjectId = d.ItemDemand.ProjectId,
                    ProjectCode = d.ItemDemand.Project != null ? d.ItemDemand.Project.ProjectCode : "",
                    ProjectName = d.ItemDemand.Project != null ? d.ItemDemand.Project.ProjectName : "",
                    ItemCode = d.Item != null ? d.Item.ItemCode : d.ItemExplanation,
                    ItemName = d.Item != null ? d.Item.ItemName : d.ItemExplanation,
                    ItemDemandNo = d.ItemDemand.ReceiptNo,
                    UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                    UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                    StatusText = d.DemandStatus == 0 ? "Onay bekleniyor" : 
                                    d.DemandStatus == 1 ? "Onaylandı" :
                                    d.DemandStatus == 2 ? "Sipariş oluşturuldu" :
                                    d.DemandStatus == 3 ? "Sipariş teslim alındı" :
                                    d.DemandStatus == 4 ? "İptal edildi" : 
                                    d.DemandStatus == 5 ? "Teklif bekleniyor" : 
                                    d.DemandStatus == 6 ? "Sipariş iletildi" :
                                    d.DemandStatus == 7 ? "Kısmi teslim alındı" : "",
                    DemandDate = d.ItemDemand.ReceiptDate,
                    DeadlineDate = d.ItemDemand.DeadlineDate,
                })
                .OrderByDescending(d => d.DemandDate)
                .ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("MyDemands")]
        [Authorize(Policy = "WebUser")]
        public IEnumerable<ItemDemandDetailModel> GetMyDemands(){
            int? userId = HekaHelpers.GetUserId(Request.HttpContext);

            ItemDemandDetailModel[] data = new ItemDemandDetailModel[0];
            try
            {
                data = _context.ItemDemandDetail.Where(d => userId != null && d.ItemDemand.SysUserId == userId).Select(d => new ItemDemandDetailModel{
                    Id = d.Id,
                    DemandStatus = d.DemandStatus,
                    Explanation = d.Explanation,
                    ItemDemandId = d.ItemDemandId,
                    ItemExplanation = d.ItemExplanation,
                    ItemId = d.ItemId,
                    LineNumber = d.LineNumber,
                    NetQuantity = d.NetQuantity,
                    Quantity = d.Quantity,
                    UnitId = d.UnitId,
                    PartDimensions = d.PartDimensions,
                    UserCode = d.ItemDemand.SysUser != null ? d.ItemDemand.SysUser.UserCode : "",
                    UserName = d.ItemDemand.SysUser != null ? d.ItemDemand.SysUser.UserName : "",
                    PartNo = d.PartNo,
                    IsContracted = d.IsContracted,
                    ProjectId = d.ItemDemand.ProjectId,
                    ProjectCode = d.ItemDemand.Project != null ? d.ItemDemand.Project.ProjectCode : "",
                    ProjectName = d.ItemDemand.Project != null ? d.ItemDemand.Project.ProjectName : "",
                    ItemCode = d.Item != null ? d.Item.ItemCode : d.ItemExplanation,
                    ItemName = d.Item != null ? d.Item.ItemName : d.ItemExplanation,
                    ItemDemandNo = d.ItemDemand.ReceiptNo,
                    UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                    UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                    StatusText = d.DemandStatus == 0 ? "Onay bekleniyor" : 
                                    d.DemandStatus == 1 ? "Onaylandı" :
                                    d.DemandStatus == 2 ? "Sipariş oluşturuldu" :
                                    d.DemandStatus == 3 ? "Sipariş teslim alındı" :
                                    d.DemandStatus == 4 ? "İptal edildi" : 
                                    d.DemandStatus == 5 ? "Teklif bekleniyor" : 
                                    d.DemandStatus == 6 ? "Sipariş iletildi" :
                                    d.DemandStatus == 7 ? "Kısmi teslim alındı" : "",
                    DemandDate = d.ItemDemand.ReceiptDate,
                    DeadlineDate = d.ItemDemand.DeadlineDate,
                })
                .OrderByDescending(d => d.DemandDate)
                .ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Detail/{id}")]
        [Authorize(Policy= "WebUser")]
        public ItemDemandDetailModel GetDetail(int id){
            ItemDemandDetailModel model = new ItemDemandDetailModel();

            try
            {
                var dbObj = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == id);
                if (dbObj != null){
                    dbObj.MapTo(model);
                }
            }
            catch (System.Exception)
            {
                
            }

            return model;
        }


        [HttpGet]
        [Route("ApprovedDetails")]
        [Authorize(Policy = "WebUser")]
        public IEnumerable<ItemDemandDetailModel> ApprovedDetails(int projectId){
            ItemDemandDetailModel[] data = new ItemDemandDetailModel[0];
            try
            {
                data = _context.ItemDemandDetail.Where(d => (d.DemandStatus ?? 0) == 0).Select(d => new ItemDemandDetailModel{
                    Id = d.Id,
                    DemandStatus = d.DemandStatus,
                    Explanation = d.Explanation,
                    ItemDemandId = d.ItemDemandId,
                    ItemExplanation = d.ItemExplanation,
                    ItemId = d.ItemId,
                    LineNumber = d.LineNumber,
                    NetQuantity = d.NetQuantity,
                    Quantity = d.Quantity,
                    UnitId = d.UnitId,
                    PartDimensions = d.PartDimensions,
                    UserCode = d.ItemDemand.SysUser != null ? d.ItemDemand.SysUser.UserCode : "",
                    UserName = d.ItemDemand.SysUser != null ? d.ItemDemand.SysUser.UserName : "",
                    PartNo = d.PartNo,
                    IsContracted = d.IsContracted,
                    ItemCode = d.Item != null ? d.Item.ItemCode : d.ItemExplanation,
                    ItemName = d.Item != null ? d.Item.ItemName : d.ItemExplanation,
                    ItemDemandNo = d.ItemDemand.ReceiptNo,
                    UnitCode = d.UnitType != null ? d.UnitType.UnitTypeCode : "",
                    UnitName = d.UnitType != null ? d.UnitType.UnitTypeName : "",
                    StatusText = d.DemandStatus == 0 ? "Onay bekleniyor" : 
                                    d.DemandStatus == 1 ? "Onaylandı" :
                                    d.DemandStatus == 2 ? "Sipariş oluşturuldu" :
                                    d.DemandStatus == 3 ? "Sipariş teslim alındı" :
                                    d.DemandStatus == 4 ? "İptal edildi" : 
                                    d.DemandStatus == 5 ? "Teklif bekleniyor" : 
                                    d.DemandStatus == 6 ? "Sipariş iletildi" :
                                    d.DemandStatus == 7 ? "Kısmi teslim alındı" : "",
                    DemandDate = d.ItemDemand.ReceiptDate,
                    DeadlineDate = d.ItemDemand.DeadlineDate,
                    ProjectId = d.ItemDemand.ProjectId,
                    ProjectCode = d.ItemDemand.Project != null ? d.ItemDemand.Project.ProjectCode : "",
                    ProjectName = d.ItemDemand.Project != null ? d.ItemDemand.Project.ProjectName : "",
                })
                .OrderByDescending(d => d.DemandDate)
                .ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }
        
        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(ItemDemandModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                // assign demand creator
                int? creatorId = HekaHelpers.GetUserId(Request.HttpContext);
                if (model.Id <= 0)
                    model.SysUserId = creatorId;

                if(model.ReceiptDate != null){
                    model.LastUpdateDate = DateTime.Now;
                }

                if (model.ReceiptDate == null){
                    model.ReceiptDate = DateTime.Now;
                }

                if (!_context.Plant.Any(d => d.Id == (model.PlantId ?? 0)))
                    model.PlantId = null;
                
                // if (model.DeadlineDate == null)
                //     throw new Exception("İhtiyaç tarihi belirtilmelidir.");

                var dbObj = _context.ItemDemand.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    model.ReceiptNo = GetNextDemandNumber();

                    dbObj = new ItemDemand();
                    dbObj.ReceiptNo = model.ReceiptNo;
                    _context.ItemDemand.Add(dbObj);
                }

                // keep constants
                var currentRcNo = dbObj.ReceiptNo;

                model.MapTo(dbObj);

                // replace constants after auto mapping
                dbObj.ReceiptNo = currentRcNo;

                #region SAVE DETAILS
                foreach (var item in model.Details)
                {
                    if (item.NewDetail)
                        item.Id = 0;
                }

                var currentDetails = _context.ItemDemandDetail.Where(d => d.ItemDemandId == dbObj.Id).ToArray();

                var removedDetails = currentDetails.Where(d => !model.Details.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedDetails)
                {
                    if (_context.ItemOrderDetail.Any(d => d.ItemDemandDetailId == item.Id))
                        throw new Exception((item.LineNumber ?? 0).ToString() + ". satırdaki talep siparişe dönüştürüldüğü için silinemez. Bu sipariş kalemini sildikten sonra talebi silebilirsiniz.");

                    _context.ItemDemandDetail.Remove(item);
                }

                foreach (var item in model.Details)
                {
                    var dbDetail = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == item.Id);
                    if (dbDetail == null){
                        dbDetail = new ItemDemandDetail();
                        _context.ItemDemandDetail.Add(dbDetail);
                    }

                    item.MapTo(dbDetail);
                    dbDetail.ItemDemand = dbObj;
                    dbDetail.IsContracted = dbObj.IsContracted;
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
        [HttpPost]
        [Route("SaveDetail")]
        public BusinessResult PostDetail(ItemDemandDetailModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                // assign demand creator
                int? creatorId = HekaHelpers.GetUserId(Request.HttpContext);
                

                var dbObj = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    throw new Exception("Düzenlenmesi istenen talep detayı bulunamadı.");
                }

                // model.MapTo(dbObj);
                dbObj.ItemId = model.ItemId;
                dbObj.ItemExplanation = model.ItemExplanation;
                dbObj.PartNo = model.PartNo;
                dbObj.PartDimensions = model.PartDimensions;
                dbObj.Quantity = model.Quantity;

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
        [Route("ApproveDetails")]
        [HttpPost]
        public BusinessResult ApproveDemandDetails(int[] detailId){
            BusinessResult result = new BusinessResult();

            try
            {
                ItemDemand dmnHeader = null;

                foreach (var dId in detailId)
                {
                    var dbObj = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == dId);
                    if (dbObj != null){
                        dbObj.DemandStatus = 1;

                        if (dmnHeader == null)
                            dmnHeader = dbObj.ItemDemand;
                    }
                }

                if (dmnHeader != null){
                    if (!_context.ItemDemandDetail.Any(d => d.ItemDemandId == dmnHeader.Id && d.DemandStatus != 1)){
                        dmnHeader.DemandStatus = 1;
                    }
                }

                _context.SaveChanges();
                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        [Authorize(Policy = "WebUser")]
        [Route("DenyDetails")]
        [HttpPost]
        public BusinessResult DenyDemandDetails(int[] detailId){
            BusinessResult result = new BusinessResult();

            try
            {
                ItemDemand dmnHeader = null;

                foreach (var dId in detailId)
                {
                    var dbObj = _context.ItemDemandDetail.FirstOrDefault(d => d.Id == dId);
                    if (dbObj != null){
                        dbObj.DemandStatus = 4;

                        if (dmnHeader == null)
                            dmnHeader = dbObj.ItemDemand;
                    }
                }

                if (dmnHeader != null){
                    if (!_context.ItemDemandDetail.Any(d => d.ItemDemandId == dmnHeader.Id && d.DemandStatus != 4)){
                        dmnHeader.DemandStatus = 4;
                    }
                }

                _context.SaveChanges();
                result.Result=true;
            }
            catch (System.Exception ex)
            {
                result.Result = false;
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
                var dbObj = _context.ItemDemand.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("Silinmesi istenen talep bilgisi bulunamadı.");

                var details = _context.ItemDemandDetail.Where(d => d.ItemDemandId == id).ToArray();
                foreach (var item in details)
                {
                    if (_context.ItemOrderDetail.Any(d => d.ItemDemandDetailId == item.Id))
                        throw new Exception((item.LineNumber ?? 0).ToString() + ". satırdaki talep siparişe dönüştürüldüğü için silinemez. Bu sipariş kalemini sildikten sonra talebi silebilirsiniz.");

                    _context.ItemDemandDetail.Remove(item);
                }

                if (_context.ItemOrder.Any(d => d.ItemDemandId == id))
                    throw new Exception("Bu talep siparişe dönüştürüldüğü için silinemez. İlgili siparişi sildikten sonra bu talebi silebilirsiiz.");

                _context.ItemDemand.Remove(dbObj);

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