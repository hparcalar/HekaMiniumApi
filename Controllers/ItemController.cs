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
    public class ItemController : HekaControllerBase{
        public ItemController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<ItemModel> Get()
        {
            ItemModel[] data = new ItemModel[0];
            try
            {
                data = _context.Item.Select(d => new ItemModel{
                    Id = d.Id,
                    Barcode = d.Barcode,
                    BrandCode = d.Brand != null ? d.Brand.BrandCode : "",
                    BrandId = d.BrandId,
                    BrandName = d.Brand != null ? d.Brand.BrandName : "",
                    BrandModelCode = d.BrandModel != null ? d.BrandModel.BrandModelCode : "",
                    BrandModelId = d.BrandModelId,
                    BrandModelName = d.BrandModel != null ? d.BrandModel.BrandModelName : "",
                    DefaultTaxRate = d.DefaultTaxRate,
                    ExpirationDate = d.ExpirationDate,
                    Explanation = d.Explanation,
                    IsActive = d.IsActive,
                    ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                    ItemCategoryId = d.ItemCategoryId,
                    ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                    ItemCode = d.ItemCode,
                    ItemName = d.ItemName,
                    ItemGroupCode = d.ItemGroup != null ? d.ItemGroup.ItemGroupCode : "",
                    ItemGroupId = d.ItemGroupId,
                    ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                    ItemType = d.ItemType,
                    PlantId = d.PlantId,
                    ProductionDate = d.ProductionDate,
                    RecordIcon = d.RecordIcon,
                    SerialNo = d.SerialNo,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Category")]
        public IEnumerable<ItemCategoryModel> GetCategories()
        {
            ItemCategoryModel[] data = new ItemCategoryModel[0];
            try
            {
                data = _context.ItemCategory.Select(d => new ItemCategoryModel{
                    Id = d.Id,
                    IsActive = d.IsActive,
                    PlantId = d.PlantId,
                    ItemCategoryCode = d.ItemCategoryCode,
                    ItemCategoryName = d.ItemCategoryName,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("{id}")]
        public ItemModel Get(int id)
        {
            ItemModel data = new ItemModel();
            try
            {
                data = _context.Item.Where(d => d.Id == id).Select(d => new ItemModel{
                        Id = d.Id,
                        Barcode = d.Barcode,
                        BrandCode = d.Brand != null ? d.Brand.BrandCode : "",
                        BrandId = d.BrandId,
                        BrandName = d.Brand != null ? d.Brand.BrandName : "",
                        BrandModelCode = d.BrandModel != null ? d.BrandModel.BrandModelCode : "",
                        BrandModelId = d.BrandModelId,
                        BrandModelName = d.BrandModel != null ? d.BrandModel.BrandModelName : "",
                        DefaultTaxRate = d.DefaultTaxRate,
                        ExpirationDate = d.ExpirationDate,
                        Explanation = d.Explanation,
                        IsActive = d.IsActive,
                        ItemCategoryCode = d.ItemCategory != null ? d.ItemCategory.ItemCategoryCode : "",
                        ItemCategoryId = d.ItemCategoryId,
                        ItemCategoryName = d.ItemCategory != null ? d.ItemCategory.ItemCategoryName : "",
                        ItemCode = d.ItemCode,
                        ItemName = d.ItemName,
                        ItemGroupCode = d.ItemGroup != null ? d.ItemGroup.ItemGroupCode : "",
                        ItemGroupId = d.ItemGroupId,
                        ItemGroupName = d.ItemGroup != null ? d.ItemGroup.ItemGroupName : "",
                        ItemType = d.ItemType,
                        PlantId = d.PlantId,
                        ProductionDate = d.ProductionDate,
                        RecordIcon = d.RecordIcon,
                        SerialNo = d.SerialNo,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Category/{id}")]
        public ItemCategoryModel GetCategory(int id)
        {
            ItemCategoryModel data = new ItemCategoryModel();
            try
            {
                data = _context.ItemCategory.Where(d => d.Id == id).Select(d => new ItemCategoryModel{
                        Id = d.Id,
                        IsActive = d.IsActive,
                        PlantId = d.PlantId,
                        ItemCategoryCode = d.ItemCategoryCode,
                        ItemCategoryName = d.ItemCategoryName,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Category/{id}/Groups")]
        public IEnumerable<ItemGroupModel> GetGroups(int id)
        {
            ItemGroupModel[] data = new ItemGroupModel[0];
            try
            {
                data = _context.ItemGroup.Where(d => d.ItemCategoryId == id)
                .Select(d => new ItemGroupModel{
                    Id = d.Id,
                    IsActive = d.IsActive,
                    ItemGroupCode = d.ItemGroupCode,
                    ItemGroupName = d.ItemGroupName,
                    Explanation = d.Explanation,
                    ItemCategoryId = d.ItemCategoryId,
                    OrderInCategory = d.OrderInCategory,
                    RecordIcon = d.RecordIcon,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }

        [HttpGet]
        [Route("Category/{id}/Groups/{groupId}")]
        public ItemGroupModel GetGroup(int id, int groupId)
        {
            ItemGroupModel data = new ItemGroupModel();
            try
            {
                data = _context.ItemGroup.Where(d => d.Id == groupId).Select(d => new ItemGroupModel{
                    Id = d.Id,
                    IsActive = d.IsActive,
                    ItemGroupCode = d.ItemGroupCode,
                    ItemGroupName = d.ItemGroupName,
                    Explanation = d.Explanation,
                    ItemCategoryId = d.ItemCategoryId,
                    OrderInCategory = d.OrderInCategory,
                    RecordIcon = d.RecordIcon,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }


        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(ItemModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.Item.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new Item();
                    _context.Item.Add(dbObj);
                }

                if (_context.Item.Any(d => d.ItemCode == model.ItemCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu stok koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

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
        public BusinessResult PostCategory(ItemCategoryModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ItemCategory.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new ItemCategory();
                    _context.ItemCategory.Add(dbObj);
                }

                if (_context.ItemCategory.Any(d => d.ItemCategoryCode == model.ItemCategoryCode && d.PlantId == model.PlantId && d.Id != model.Id))
                    throw new Exception("Bu stok kategori koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

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
                var dbObj = _context.Item.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.Item.Remove(dbObj);

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
                var dbObj = _context.ItemCategory.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.ItemCategory.Remove(dbObj);

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
        [Route("Group")]
        [HttpDelete]
        public BusinessResult DeleteGroup(int id){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.ItemGroup.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.ItemGroup.Remove(dbObj);

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