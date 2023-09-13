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
    public class AttachmentController : HekaControllerBase{
        public AttachmentController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        [Route("OfRecord/{recordType}/{recordId}")]
        public IEnumerable<AttachmentModel> Get(int recordType, int recordId)
        {
            AttachmentModel[] data = new AttachmentModel[0];
            try
            {
                data = _context.Attachment.Where(d => d.RecordType == recordType && d.RecordId == recordId)
                .Select(d => new AttachmentModel{
                    Id = d.Id,
                    Explanation = d.Explanation,
                    // FileContent = d.FileContent,
                    FileExtension = d.FileExtension,
                    IsOfferDoc = d.IsOfferDoc,
                    FileName = d.FileName,
                    FileType = d.FileType,
                    RecordId = d.RecordId,
                    RecordType = d.RecordType,
                    Title = d.Title,
                    PartNo = d.PartNo,
                    SubParts = d.SubParts,
                    AttachmentCategoryId = d.AttachmentCategoryId,
                    CategoryName = d.AttachmentCategory != null ? d.AttachmentCategory.CategoryName : "",
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("OfRecordWithContent/{recordType}/{recordId}")]
        public IEnumerable<AttachmentModel> GetWithContent(int recordType, int recordId)
        {
            AttachmentModel[] data = new AttachmentModel[0];
            try
            {
                data = _context.Attachment.Where(d => d.RecordType == recordType && d.RecordId == recordId)
                .Select(d => new AttachmentModel{
                    Id = d.Id,
                    Explanation = d.Explanation,
                    FileContent = d.FileContent,
                    FileExtension = d.FileExtension,
                    IsOfferDoc = d.IsOfferDoc,
                    FileName = d.FileName,
                    FileType = d.FileType,
                    RecordId = d.RecordId,
                    RecordType = d.RecordType,
                    Title = d.Title,
                    PartNo = d.PartNo,
                    SubParts = d.SubParts,
                    AttachmentCategoryId = d.AttachmentCategoryId,
                    CategoryName = d.AttachmentCategory != null ? d.AttachmentCategory.CategoryName : "",
                }).ToArray();

                foreach (var item in data)
                {
                    if (item.FileContent != null){
                        item.ContentBase64 = Convert.ToBase64String(item.FileContent);
                        item.FileContent = null;
                    }
                }
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("{id}")]
        public AttachmentModel Get(int id)
        {
            AttachmentModel data = new AttachmentModel();
            try
            {
                data = _context.Attachment.Where(d => d.Id == id).Select(d => new AttachmentModel{
                        Id = d.Id,
                        Explanation = d.Explanation,
                        FileContent = d.FileContent,
                        FileExtension = d.FileExtension,
                        FileName = d.FileName,
                        FileType = d.FileType,
                        IsOfferDoc = d.IsOfferDoc,
                        RecordId = d.RecordId,
                        RecordType = d.RecordType,
                        Title = d.Title,
                        PartNo = d.PartNo,
                        SubParts = d.SubParts,
                        AttachmentCategoryId = d.AttachmentCategoryId,
                        CategoryName = d.AttachmentCategory != null ? d.AttachmentCategory.CategoryName : "",
                    }).FirstOrDefault();

                if (data != null && data.FileContent != null){
                    data.ContentBase64 = Convert.ToBase64String(data.FileContent);
                }
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("Category")]
        public IEnumerable<AttachmentCategoryModel> GetCategories()
        {
            AttachmentCategoryModel[] data = new AttachmentCategoryModel[0];
            try
            {
                data = _context.AttachmentCategory.Select(d => new AttachmentCategoryModel{
                    Id = d.Id,
                    PlantId = d.PlantId,
                    CategoryName = d.CategoryName
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Category/{id}")]
        public AttachmentCategoryModel GetCategory(int id)
        {
            AttachmentCategoryModel data = new AttachmentCategoryModel();
            try
            {
                data = _context.AttachmentCategory.Where(d => d.Id == id).Select(d => new AttachmentCategoryModel{
                        Id = d.Id,
                        PlantId = d.PlantId,
                        CategoryName = d.CategoryName
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [Authorize(Policy = "WebUser")]
        [Route("Category")]
        [HttpPost]
        public BusinessResult PostCategory(AttachmentCategoryModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.AttachmentCategory.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new AttachmentCategory();
                    _context.AttachmentCategory.Add(dbObj);
                }

                if (_context.AttachmentCategory.Any(d => d.CategoryName == model.CategoryName && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu kategori adına ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

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
        public BusinessResult Post(AttachmentModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Attachment.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Attachment();
                    _context.Attachment.Add(dbObj);
                }

                // keep current content, only update other informations
                var currentContent = dbObj.FileContent;

                model.MapTo(dbObj);

                dbObj.FileContent = currentContent;

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
        [Route("Upload/{id}")]
        public BusinessResult UploadFile(int id, IFormFile fileData){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbAttachment = _context.Attachment.FirstOrDefault(d => d.Id == id);

                if (dbAttachment == null)
                    throw new Exception("Dosya eki kaydına ulaşılamadı.");

                if (fileData == null || fileData.Length <= 0)
                    throw new Exception("Yüklenecek bir dosya seçilmemiş.");

                using (var ms = new MemoryStream())
                {
                    fileData.CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    dbAttachment.FileContent = fileBytes;
                }

                dbAttachment.FileName = fileData.FileName;
                dbAttachment.FileType = fileData.ContentType;

                _context.SaveChanges();

                result.RecordId = dbAttachment.Id;
                result.Result = true;
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
                var dbObj = _context.Attachment.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.Attachment.Remove(dbObj);

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
        [HttpDelete("Category/{id}")]
        public BusinessResult DeleteCategory(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.AttachmentCategory.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.AttachmentCategory.Remove(dbObj);

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