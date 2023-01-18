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

namespace HekaMiniumApi.Controllers
{

  [Authorize]
  [ApiController]
  [Route("[controller]")]
  [EnableCors()]
  public class DepartmentController : HekaControllerBase
  {
    public DepartmentController(HekaMiniumSchema context) : base(context)
    {
      ResolveHeaders(Request);
    }

    [HttpGet]
    public IEnumerable<DepartmentModel> Get()
    {
      DepartmentModel[] data = new DepartmentModel[0];
      try
      {
        data = _context.Department.Select(d => new DepartmentModel
        {
          Id = d.Id,
          DepartmentCode = d.DepartmentCode,
          DepartmentName = d.DepartmentName,
          PlantId = d.PlantId,
        }).ToArray();
      }
      catch
      {

      }

      return data;
    }

    [HttpGet]
    [Route("{id}")]
    public DepartmentModel Get(int id)
    {
      DepartmentModel data = new DepartmentModel();
      try
      {
        data = _context.Department.Where(d => d.Id == id).Select(d => new DepartmentModel
        {
          Id = d.Id,
          DepartmentCode = d.DepartmentCode,
          DepartmentName = d.DepartmentName,
          PlantId = d.PlantId,
        }).FirstOrDefault();

        if (data == null)
        {
          data = new DepartmentModel();
          data.DepartmentCode = Convert.ToInt32(GetNextNumber());
        }
      }
      catch
      {

      }

      return data;
    }

    [Authorize(Policy = "WebUser")]
    [HttpPost]
    public BusinessResult Post(DepartmentModel model)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.Department.FirstOrDefault(d => d.Id == model.Id);
        if (dbObj == null)
        {
          dbObj = new Department();
          _context.Department.Add(dbObj);
        }

        if (_context.Department.Any(d => d.DepartmentCode == model.DepartmentCode && d.Id != model.Id))
          throw new Exception("Bu departman koduna ait bir kayıt zaten bulunmaktadır. Lütfen başka bir kod belirtiniz.");

        model.MapTo(dbObj);

        _context.SaveChanges();
        result.Result = true;
        result.RecordId = dbObj.Id;
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
    public BusinessResult Delete(int id)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.Department.FirstOrDefault(d => d.Id == id);
        if (dbObj == null)
          throw new Exception("");

        _context.Department.Remove(dbObj);

        _context.SaveChanges();
        result.Result = true;
      }
      catch (System.Exception ex)
      {
        result.Result = false;
        result.ErrorMessage = ex.Message;
      }

      return result;
    }

    private string GetNextNumber()
    {
      try
      {
        int nextNumber = 1;
        var lastRecord = _context.Department.OrderByDescending(d => d.DepartmentCode).Select(d => d.DepartmentCode).FirstOrDefault();
        if (lastRecord != null && !string.IsNullOrEmpty(lastRecord.ToString()))
          nextNumber = Convert.ToInt32(lastRecord) + 1;

        return string.Format("{0:0}", nextNumber);
      }
      catch (System.Exception)
      {

      }

      return string.Empty;
    }
  }
}