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
    public class FirmController : HekaControllerBase{
        public FirmController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<FirmModel> Get()
        {
            FirmModel[] data = new FirmModel[0];
            try
            {
                data = _context.Firm.Select(d => new FirmModel{
                    Id = d.Id,
                    IsActive = d.IsActive,
                    AddressInfoId = d.AddressInfoId,
                    CommercialTitle = d.CommercialTitle,
                    EInvoiceEndpoint = d.EInvoiceEndpoint,
                    EInvoiceLogin = d.EInvoiceLogin,
                    EInvoicePassword = d.EInvoicePassword,
                    EWaybillEndpoint = d.EWaybillEndpoint,
                    EWaybillLogin = d.EWaybillLogin,
                    EWaybillPassword = d.EWaybillPassword,
                    FirmCategoryId = d.FirmCategoryId,
                    FirmCode = d.FirmCode,
                    FirmName = d.FirmName,
                    FirmType = d.FirmType,
                    IsEInvoice = d.IsEInvoice,
                    IsEWaybill = d.IsEWaybill,
                    PlantId = d.PlantId,
                    TaxNo = d.TaxNo,
                    TaxOffice = d.TaxOffice,
                    FirmCategoryCode = d.FirmCategory != null ? d.FirmCategory.FirmCategoryCode : "",
                    FirmCategoryName = d.FirmCategory != null ? d.FirmCategory.FirmCategoryName : "",
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("Category")]
        public IEnumerable<FirmCategoryModel> GetCategories()
        {
            FirmCategoryModel[] data = new FirmCategoryModel[0];
            try
            {
                data = _context.FirmCategory.Select(d => new FirmCategoryModel{
                    Id = d.Id,
                    IsActive = d.IsActive,
                    PlantId = d.PlantId,
                    FirmCategoryCode = d.FirmCategoryCode,
                    FirmCategoryName = d.FirmCategoryName,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("{id}")]
        public FirmModel Get(int id)
        {
            FirmModel data = new FirmModel();
            try
            {
                data = _context.Firm.Where(d => d.Id == id).Select(d => new FirmModel{
                        Id = d.Id,
                        IsActive = d.IsActive,
                        AddressInfoId = d.AddressInfoId,
                        CommercialTitle = d.CommercialTitle,
                        EInvoiceEndpoint = d.EInvoiceEndpoint,
                        EInvoiceLogin = d.EInvoiceLogin,
                        EInvoicePassword = d.EInvoicePassword,
                        EWaybillEndpoint = d.EWaybillEndpoint,
                        EWaybillLogin = d.EWaybillLogin,
                        EWaybillPassword = d.EWaybillPassword,
                        FirmCategoryId = d.FirmCategoryId,
                        FirmCode = d.FirmCode,
                        FirmName = d.FirmName,
                        FirmType = d.FirmType,
                        IsEInvoice = d.IsEInvoice,
                        IsEWaybill = d.IsEWaybill,
                        PlantId = d.PlantId,
                        TaxNo = d.TaxNo,
                        TaxOffice = d.TaxOffice,
                    }).FirstOrDefault();

                if (data != null){
                    var firmAddr = _context.AddressInfo.FirstOrDefault(d => d.Id == data.AddressInfoId);
                    if (firmAddr != null){
                        data.AddressText = firmAddr.OpenAddress;
                        data.PhoneText = firmAddr.Gsm;
                        data.EmailText = firmAddr.Email;
                    }

                    var firmAuthor = _context.FirmAuthor.FirstOrDefault(d => d.FirmId == data.Id);
                    if (firmAuthor != null){
                        data.AuthorText = firmAuthor.AuthorName;
                    }
                }
                else{
                    data = new FirmModel();
                    data.FirmCode = GetNextNumber();
                }
            }
            catch
            {
                
            }
            
            return data;
        }

        private string GetNextNumber(){
            try
            {
                int nextNumber = 1;
                var lastRecord = _context.Firm.OrderByDescending(d => d.FirmCode).Select(d => d.FirmCode).FirstOrDefault();
                if (lastRecord != null && !string.IsNullOrEmpty(lastRecord))
                    nextNumber = Convert.ToInt32(lastRecord) + 1;

                return string.Format("{0:000}", nextNumber);
            }
            catch (System.Exception)
            {
                
            }

            return string.Empty;
        }


        [HttpGet]
        [Route("Category/{id}")]
        public FirmCategoryModel GetCategory(int id)
        {
            FirmCategoryModel data = new FirmCategoryModel();
            try
            {
                data = _context.FirmCategory.Where(d => d.Id == id).Select(d => new FirmCategoryModel{
                        Id = d.Id,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        FirmCategoryCode = d.FirmCategoryCode,
                        FirmCategoryName = d.FirmCategoryName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }


        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(FirmModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Firm.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Firm();
                    _context.Firm.Add(dbObj);
                }

                if (_context.Firm.Any(d => d.FirmCode == model.FirmCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu firma koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

                model.MapTo(dbObj);

                # region UPDATE ADDR & AUTHOR INFORMATION
                var addrInfo = _context.AddressInfo.FirstOrDefault(d => d.Id == dbObj.AddressInfoId);
                if (addrInfo == null){
                    addrInfo = new AddressInfo{
                        OpenAddress = model.AddressText,
                        Gsm = model.PhoneText,
                        Email = model.EmailText,
                    };
                    _context.AddressInfo.Add(addrInfo);

                    dbObj.AddressInfo = addrInfo;
                }
                else{
                    addrInfo.OpenAddress = model.AddressText;
                    addrInfo.Gsm = model.PhoneText;
                    addrInfo.Email = model.EmailText;
                }

                var authorInfo = _context.FirmAuthor.FirstOrDefault(d => d.Firm == dbObj);
                if (authorInfo == null){
                    authorInfo = new FirmAuthor{
                        AuthorName = model.AuthorText,
                    };
                    authorInfo.Firm = dbObj;
                    _context.FirmAuthor.Add(authorInfo);
                }
                else{
                    authorInfo.AuthorName = model.AuthorText;
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
        public BusinessResult PostCategory(FirmCategoryModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.FirmCategory.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new FirmCategory();
                    _context.FirmCategory.Add(dbObj);
                }

                if (_context.FirmCategory.Any(d => d.FirmCategoryCode == model.FirmCategoryCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu firma kategori koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

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
        [HttpDelete("{id}")]
        public BusinessResult Delete(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Firm.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.Firm.Remove(dbObj);

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
        [HttpDelete("{id}")]
        public BusinessResult DeleteCategory(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.FirmCategory.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.FirmCategory.Remove(dbObj);

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