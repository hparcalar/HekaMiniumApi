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
                    CloudDocId = d.CloudDocId,
                    ResponsiblePerson = d.ResponsiblePerson,
                    ProjectCategoryCode = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryCode : "",
                    ProjectCategoryName = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryName : "",
                    ProjectStatus = d.ProjectStatus,
                    ForexId = d.ForexId,
                    ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                    ForexName = d.Forex != null ? d.Forex.ForexName : "",
                    TotalCost = d.TotalCost,
                    TotalForexCost = d.TotalForexCost,
                    Quantity = d.Quantity,
                    ProfitRate = d.ProfitRate,
                    OfferPrice = d.OfferPrice,
                    ForexRate = d.ForexRate,
                    OfferForexPrice = d.OfferForexPrice,
                    IsInvoiced = d.IsInvoiced,
                    ExpiryExplanation = d.ExpiryExplanation,
                    ExpiryStartDate = d.ExpiryStartDate,
                    ExpiryTime = d.ExpiryTime,
                    ExpiryEndDate = d.ExpiryEndDate,
                    InvoicePrice = d.InvoicePrice,
                    InvoiceForexPrice = d.InvoiceForexPrice,
                    ProjectStatusText = (d.ProjectStatus ?? 0) == 0 ? "Oluşturuldu" :
                                            d.ProjectStatus == 1 ? "Teklif verilecek" :
                                            d.ProjectStatus == 2 ? "Teklif verildi" :
                                            d.ProjectStatus == 3 ? "Onaylandı" :
                                            d.ProjectStatus == 4 ? "Tamamlandı" : 
                                            d.ProjectStatus == 5 ? "İptal edildi" : ""
                }).OrderByDescending(d => d.ProjectCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("WithDocs")]
        public IEnumerable<ProjectModel> GetWithDocs()
        {
            ProjectModel[] data = new ProjectModel[0];
            try
            {
                var prByPartDocs = _context.ItemDemandDetailPart.Where(d =>
                    d.PartFile != null
                ).Select(d => d.ItemDemandDetail.ItemDemand.ProjectId).Distinct().ToArray();

                var demandByAttachments = _context.Attachment.Where(d => d.RecordType == 2)
                    .Select(d => d.RecordId).Distinct().ToArray();
                
                var prByDemandDocs = _context.ItemDemandDetail.Where(d => 
                    (demandByAttachments.Contains(d.Id) || d.ItemDemandProcesses.Any(m => m.Process.ProcessType == 1)) 
                    && d.ItemDemand.ProjectId != null)
                    .Select(d => d.ItemDemand.ProjectId).Distinct().ToArray();
                var prByPrDocs = _context.Attachment.Where(d => d.RecordType == 1)
                    .Select(d => d.RecordId).Distinct().ToArray();

                int?[] prId = prByPartDocs.Concat(prByDemandDocs).Concat(prByPrDocs).ToArray();

                data = _context.Project.Where(d => 
                    prId.Contains(d.Id)
                ).Select(d => new ProjectModel{
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
                    CloudDocId = d.CloudDocId,
                    ResponsibleInfo = d.ResponsibleInfo,
                    ResponsiblePerson = d.ResponsiblePerson,
                    ProjectCategoryCode = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryCode : "",
                    ProjectCategoryName = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryName : "",
                    ProjectStatus = d.ProjectStatus,
                    ForexId = d.ForexId,
                    ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                    ForexName = d.Forex != null ? d.Forex.ForexName : "",
                    Quantity = d.Quantity,
                    TotalCost = d.TotalCost,
                        TotalForexCost = d.TotalForexCost,
                    ProfitRate = d.ProfitRate,
                    OfferPrice = d.OfferPrice,
                    ForexRate = d.ForexRate,
                    OfferForexPrice = d.OfferForexPrice,
                    ProjectStatusText = (d.ProjectStatus ?? 0) == 0 ? "Oluşturuldu" :
                                            d.ProjectStatus == 1 ? "Teklif verilecek" :
                                            d.ProjectStatus == 2 ? "Teklif verildi" :
                                            d.ProjectStatus == 3 ? "Onaylandı" :
                                            d.ProjectStatus == 4 ? "Tamamlandı" : 
                                            d.ProjectStatus == 5 ? "İptal edildi" : ""
                }).OrderByDescending(d => d.ProjectCode).ToArray();
            }
            catch (Exception ex)
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}/DocumentCategories")]
        public IEnumerable<ItemModel> GetDocumentCategories(int id){
            ItemModel[] data = new ItemModel[0];

            try
            {
                int?[] itByParts = _context.ItemDemandDetail
                    .Where(d => d.ItemId != null && d.ItemDemand.ProjectId == id 
                        && (d.ItemDemandDetailParts.Any(m => m.PartFile != null) || d.ItemDemandProcesses.Any(m => m.Process.ProcessType == 1))
                        )
                    .Select(d => d.ItemId).Distinct().ToArray();

                int?[] demandsByProject = _context.ItemDemandDetail.Where(d => d.ItemDemand.ProjectId == id)
                    .Select(d => (int?)d.Id).ToArray();

                int?[] demandsHasDocs = _context.Attachment.Where(d => d.RecordType == 2 && demandsByProject.Contains(d.RecordId ?? 0))
                    .Select(d => d.RecordId).ToArray();

                int?[] itByDemandsWithDocs = _context.ItemDemandDetail.Where(d => d.ItemId != null && demandsHasDocs.Contains(d.Id))
                    .Select(d => (int?)d.ItemId).ToArray();

                int?[] itList = itByParts.Concat(itByDemandsWithDocs).ToArray();

                data = _context.Item.Where(d => itList.Contains(d.Id))
                    .Select(d => new ItemModel{
                        Id = d.Id,
                        ItemCode = d.ItemCode,
                        ItemName = d.ItemName,
                        ItemCategoryId = d.ItemCategoryId,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                    }).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [HttpGet]
        [Route("{id}/ElementsOfCategory/{itemId}")]
        public IEnumerable<ItemDemandDetailModel> GetElementsOfCategory(int id, int itemId){
            ItemDemandDetailModel[] data = new ItemDemandDetailModel[0];

            try
            {
                var demandsWithCurrentId = _context.ItemDemandDetail.Where(d => d.ItemId == itemId
                    && d.ItemDemand.ProjectId == id).Select(d => d.Id).ToArray();
                var hasAttachmentsDemands = _context.Attachment.Where(d => d.RecordType == 2 && demandsWithCurrentId.Contains(d.RecordId ?? 0))
                    .Select(d => d.RecordId).ToArray();

                data = _context.ItemDemandDetail
                    .Where(d => d.ItemDemand.ProjectId == id && d.ItemId == itemId 
                        &&
                        (
                            d.ItemDemandProcesses.Any(m => m.Process.ProcessType == 1)
                            ||
                            d.ItemDemandDetailParts.Any(m => m.PartFile != null)
                            ||
                            hasAttachmentsDemands.Contains(d.Id)
                        )
                    ).Select(d => new ItemDemandDetailModel{
                        Id = d.Id,
                        PartNo = d.PartNo,
                        ItemExplanation = d.ItemExplanation,
                        PartDimensions = d.ItemDemandDetailParts.Any() ? (d.PartWidth + "x" + d.PartHeight + "x" + d.PartThickness) : d.PartDimensions,
                        Quantity = d.Quantity,
                    }).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }


        [HttpGet]
        [Route("{id}/Documents/{demandDetailId}")]
        public IEnumerable<ItemDemandDetailPartModel> GetDocumentsOfElement(int id, int demandDetailId){
            ItemDemandDetailPartModel[] data = new ItemDemandDetailPartModel[0];

            try
            {
                var partFiles = _context.ItemDemandDetailPart.Where(d =>
                    d.ItemDemandDetailId == demandDetailId)
                    .Select(d => new ItemDemandDetailPartModel{
                        Id = d.Id,
                        PartNo = d.PartNo,
                        PartHeight = d.PartHeight,
                        PartQuantity = d.PartQuantity,
                        LineNumber = d.LineNumber,
                        PartType = d.PartType,
                        FileType = d.FileType,
                    }).ToArray();

                var detailFiles = _context.Attachment.Where(d => d.RecordType == 2 && d.RecordId == demandDetailId)
                    .Select(d => new ItemDemandDetailPartModel{
                        Id = 0,
                        AttachmentId = d.Id,
                        // PartFile = d.FileContent,
                        PartNo = d.PartNo + " " + d.Title + " / " + d.Explanation,
                        FileType = d.FileType,
                    }
                    ).ToArray();
                
                data = detailFiles.Concat(partFiles).ToArray();
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [HttpGet]
        [Route("{id}/PartDocument/{partId}")]
        public ItemDemandDetailPartModel GetPartDocument(int id, int partId){
            ItemDemandDetailPartModel data = new ItemDemandDetailPartModel();

            try
            {
                data = _context.ItemDemandDetailPart.Where(d =>
                    d.Id == partId)
                    .Select(d => new ItemDemandDetailPartModel{
                        Id = d.Id,
                        PartNo = d.PartNo,
                        PartHeight = d.PartHeight,
                        PartQuantity = d.PartQuantity,
                        LineNumber = d.LineNumber,
                        PartType = d.PartType,
                        FileType = d.FileType,
                        PartFile = d.PartFile,
                    }).FirstOrDefault();

                if (data != null){
                    data.PartBase64 = Convert.ToBase64String(data.PartFile);
                }
            }
            catch (System.Exception)
            {
                
            }

            return data;
        }

        [HttpGet]
        [Route("AfterCreated")]
        public IEnumerable<ProjectModel> GetAfterCreated()
        {
            ProjectModel[] data = new ProjectModel[0];
            try
            {
                data = _context.Project.Where(d => d.ProjectStatus > 0).Select(d => new ProjectModel{
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
                    CloudDocId = d.CloudDocId,
                    ResponsibleInfo = d.ResponsibleInfo,
                    ResponsiblePerson = d.ResponsiblePerson,
                    ProjectCategoryCode = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryCode : "",
                    ProjectCategoryName = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryName : "",
                    ProjectStatus = d.ProjectStatus,
                    ForexId = d.ForexId,
                    ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                    ForexName = d.Forex != null ? d.Forex.ForexName : "",
                    Quantity = d.Quantity,
                    TotalCost = d.TotalCost,
                        TotalForexCost = d.TotalForexCost,
                    ProfitRate = d.ProfitRate,
                    OfferPrice = d.OfferPrice,
                    ForexRate = d.ForexRate,
                    OfferForexPrice = d.OfferForexPrice,
                    ProjectStatusText = (d.ProjectStatus ?? 0) == 0 ? "Oluşturuldu" :
                                            d.ProjectStatus == 1 ? "Teklif verilecek" :
                                            d.ProjectStatus == 2 ? "Teklif verildi" :
                                            d.ProjectStatus == 3 ? "Onaylandı" :
                                            d.ProjectStatus == 4 ? "Tamamlandı" : 
                                            d.ProjectStatus == 5 ? "İptal edildi" : ""
                }).OrderByDescending(d => d.ProjectCode).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Demandable")]
        public IEnumerable<ProjectModel> GetDemandable()
        {
            ProjectModel[] data = new ProjectModel[0];
            try
            {
                data = _context.Project.Where(d => d.ProjectStatus == 3 || d.ProjectStatus == 4).Select(d => new ProjectModel{
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
                    CloudDocId = d.CloudDocId,
                    ProjectCategoryId = d.ProjectCategoryId,
                    ProjectCode = d.ProjectCode,
                    ProjectName = d.ProjectName,
                    ProjectPhaseTemplateId = d.ProjectPhaseTemplateId,
                    ResponsibleInfo = d.ResponsibleInfo,
                    ResponsiblePerson = d.ResponsiblePerson,
                    ProjectCategoryCode = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryCode : "",
                    ProjectCategoryName = d.ProjectCategory != null ? d.ProjectCategory.ProjectCategoryName : "",
                    ProjectStatus = d.ProjectStatus,
                    ForexId = d.ForexId,
                    ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                    ForexName = d.Forex != null ? d.Forex.ForexName : "",
                    TotalCost = d.TotalCost,
                        TotalForexCost = d.TotalForexCost,
                    Quantity = d.Quantity,
                    ProfitRate = d.ProfitRate,
                    OfferPrice = d.OfferPrice,
                    ForexRate = d.ForexRate,
                    OfferForexPrice = d.OfferForexPrice,
                    ProjectStatusText = (d.ProjectStatus ?? 0) == 0 ? "Oluşturuldu" :
                                            d.ProjectStatus == 1 ? "Teklif verilecek" :
                                            d.ProjectStatus == 2 ? "Teklif verildi" :
                                            d.ProjectStatus == 3 ? "Onaylandı" :
                                            d.ProjectStatus == 4 ? "Tamamlandı" : 
                                            d.ProjectStatus == 5 ? "İptal edildi" : ""
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

        private string GetNextProjectNumber(){
            try
            {
                string currentYear = string.Format("{0:yyyy}", DateTime.Now).Substring(2);

                int nextNumber = 1;
                var lastRecord = _context.Project.Where(d => d.ProjectCode.Substring(0,2).Contains(currentYear))
                    .OrderByDescending(d => d.ProjectCode.Substring(3)).Select(d => d.ProjectCode.Substring(3)).FirstOrDefault();
                if (lastRecord != null && !string.IsNullOrEmpty(lastRecord))
                    nextNumber = Convert.ToInt32(lastRecord) + 1;

                return currentYear + "-" + string.Format("{0:000000}", nextNumber);
            }
            catch (System.Exception)
            {
                
            }

            return string.Empty;
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
                        CriticalExplanation = d.CriticalExplanation,
                        OfferType = d.OfferType,
                        Explanation = d.Explanation,
                        MeetingExplanation = d.MeetingExplanation,
                        ProjectPhaseTemplateId = d.ProjectPhaseTemplateId,
                        ResponsibleInfo = d.ResponsibleInfo,
                        ResponsiblePerson = d.ResponsiblePerson,
                        CloudDocId = d.CloudDocId,
                        CloudSheetId = d.CloudSheetId,
                        ProjectStatus = d.ProjectStatus,
                        ForexId = d.ForexId,
                        ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                        ForexName = d.Forex != null ? d.Forex.ForexName : "",
                        Quantity = d.Quantity,
                        ProfitRate = d.ProfitRate,
                        OfferPrice = d.OfferPrice,
                        ForexRate = d.ForexRate,
                        OfferForexPrice = d.OfferForexPrice,
                        TotalCost = d.TotalCost,
                        TotalForexCost = d.TotalForexCost,
                        IsInvoiced = d.IsInvoiced,
                        ExpiryExplanation = d.ExpiryExplanation,
                        ExpiryStartDate = d.ExpiryStartDate,
                        ExpiryTime = d.ExpiryTime,
                        ExpiryEndDate = d.ExpiryEndDate,
                        InvoicePrice = d.InvoicePrice,
                        InvoiceForexPrice = d.InvoiceForexPrice,
                        ProjectStatusText = (d.ProjectStatus ?? 0) == 0 ? "Teklif verilecek" :
                                            d.ProjectStatus == 1 ? "Teklif verildi" :
                                            d.ProjectStatus == 2 ? "Onaylandı" :
                                            d.ProjectStatus == 3 ? "Tamamlandı" : 
                                            d.ProjectStatus == 4 ? "İptal edildi" : ""
                    }).FirstOrDefault();

                if (data != null && data.Id > 0){
                    data.CostItems = _context.ProjectCostItem.Where(d => d.ProjectId == id)
                        .Select(d => new ProjectCostItemModel{
                            Id = d.Id,
                            CostName = d.CostName,
                            CostStatus = d.CostStatus,
                            CostType = d.CostType,
                            CreatedDate = d.CreatedDate,
                            CreatedUserId = d.CreatedUserId,
                            DiscountRate = d.DiscountRate,
                            EstimatedForexOverallTotal = d.EstimatedForexOverallTotal,
                            EstimatedForexRate = d.EstimatedForexRate,
                            EstimatedForexSubTotal = d.EstimatedForexSubTotal,
                            EstimatedForexTaxTotal = d.EstimatedForexTaxTotal,
                            EstimatedForexUnitPrice = d.EstimatedForexUnitPrice,
                            EstimatedOverallTotal = d.EstimatedOverallTotal,
                            EstimatedSubTotal = d.EstimatedSubTotal,
                            EstimatedTaxTotal = d.EstimatedTaxTotal,
                            EstimatedUnitPrice = d.EstimatedUnitPrice,
                            Explanation = d.Explanation,
                            ForexCode = d.Forex != null ? d.Forex.ForexCode : "",
                            ForexId = d.ForexId,
                            ForexName = d.Forex != null ? d.Forex.ForexName : "",
                            ForexOverallTotal = d.ForexOverallTotal,
                            ForexRate = d.ForexRate,
                            ForexSubTotal = d.ForexSubTotal,
                            ForexTaxTotal = d.ForexTaxTotal,
                            ForexUnitPrice = d.ForexUnitPrice,
                            ItemCode = d.Item != null ? d.Item.ItemCode : "",
                            ItemId = d.ItemId,
                            ItemName = d.Item != null ? d.Item.ItemName : "",
                            LineNumber = d.LineNumber,
                            OverallTotal = d.OverallTotal,
                            ProjectCode = d.Project != null ? d.Project.ProjectCode : "",
                            ProjectId = d.ProjectId,
                            ProjectName = d.Project != null ? d.Project.ProjectName : "",
                            Quantity = d.Quantity,
                            RealizedDate = d.RealizedDate,
                            SubTotal = d.SubTotal,
                            TaxRate = d.TaxRate,
                            TaxTotal = d.TaxTotal,
                            UnitPrice = d.UnitPrice,
                            CostStatusText = (d.CostStatus) == 0 ? "Bekleniyor" : "Gerçekleşti",
                            CostTypeText = (d.CostType) == 0 ? "Malzeme" : "İşçilik",
                            PartNo = d.PartNo,
                            PartDimensions = d.PartDimensions,
                        }).ToArray();
               
                    data.Attachments = _context.Attachment.Where(d => d.RecordType == 1 && d.RecordId == id)
                        .Select(d => new AttachmentModel{
                            Id = d.Id,
                            Explanation = d.Explanation,
                            FileExtension = d.FileExtension,
                            FileName = d.FileName,
                            FileType = d.FileType,
                            RecordId = d.RecordId,
                            RecordType = d.RecordType,
                            Title = d.Title,
                        }).ToArray();
                }
                else{
                    if (data == null)
                        data = new ProjectModel();

                    data.ProjectCode = GetNextProjectNumber();
                    data.CostItems = new ProjectCostItemModel[0];
                    data.Attachments = new AttachmentModel[0];
                }
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
                    model.ProjectCode = GetNextProjectNumber();

                    dbObj = new Project();
                    dbObj.ProjectCode = model.ProjectCode;
                    _context.Project.Add(dbObj);
                }

                if (model.ProjectStatus == null)
                    model.ProjectStatus = 0;

                if (_context.Project.Any(d => d.ProjectCode == model.ProjectCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu proje koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

                // keep values
                string currentCode = dbObj.ProjectCode;

                model.MapTo(dbObj);

                dbObj.ProjectCode = currentCode;

                if (dbObj.DeadlineDate < dbObj.StartDate)
                    throw new Exception("Termin tarihi proje başlangıç tarihinden önce olamaz.");

                #region SAVE COST ITEMS
                var currentCostItems = _context.ProjectCostItem.Where(d => d.ProjectId == dbObj.Id).ToArray();

                var removedCostItems = currentCostItems.Where(d => !model.CostItems.Any(m => m.Id == d.Id)).ToArray();
                foreach (var item in removedCostItems)
                {
                    _context.ProjectCostItem.Remove(item);
                }

                foreach (var item in model.CostItems)
                {
                    var dbDetail = _context.ProjectCostItem.FirstOrDefault(d => d.Id == item.Id);
                    if (dbDetail == null){
                        dbDetail = new ProjectCostItem();
                        _context.ProjectCostItem.Add(dbDetail);
                    }

                    item.MapTo(dbDetail);
                    dbDetail.Project = dbObj;
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
        [HttpPost]
        [Route("ExpiryEdit")]
        [AllowAnonymous]
        public BusinessResult ExpiryEdit(ProjectModel model)
        {
        BusinessResult result = new BusinessResult();

        try
        {
            var dbObj = _context.Project.FirstOrDefault(d => d.Id == model.Id);
            if (dbObj == null){
                throw new Exception("Bu koda ait proje bulunamadı.");
            }
            else {
                dbObj.ExpiryExplanation = model.ExpiryExplanation;
                dbObj.ExpiryStartDate = model.ExpiryStartDate;
                dbObj.ExpiryEndDate = model.ExpiryEndDate;
                dbObj.ExpiryTime = model.ExpiryTime;
                dbObj.IsInvoiced = model.IsInvoiced;
                dbObj.InvoicePrice = model.InvoicePrice;
                dbObj.InvoiceForexPrice = model.InvoiceForexPrice;
            
                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.Id;
            }
        }
        catch (System.Exception ex)
        {
            result.Result = false;
            result.ErrorMessage = ex.Message;
        }

        return result;
        }

        [Authorize(Policy = "WebUser")]
        [HttpPost]
        [Route("ChangeInvoiceStatus")]
        [AllowAnonymous]
        public BusinessResult ChangeInvoiceStatus(ProjectModel model)
        {
        BusinessResult result = new BusinessResult();

        try
        {
            var dbObj = _context.Project.FirstOrDefault(d => d.Id == model.Id);
            if (dbObj == null){
                throw new Exception("Bu koda ait proje bulunamadı.");
            }
            else {
                dbObj.IsInvoiced = model.IsInvoiced;
                _context.SaveChanges();
                result.Result=true;
                result.RecordId = dbObj.Id;
            }
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
                var dbObj = _context.Project.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                var costItems = _context.ProjectCostItem.Where(d => d.ProjectId == id).ToArray();
                foreach (var item in costItems)
                {
                    _context.ProjectCostItem.Remove(item);
                }

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