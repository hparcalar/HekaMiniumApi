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
  public class RemoteOfficeController : HekaControllerBase
  {
    public RemoteOfficeController(HekaMiniumSchema context) : base(context)
    {
      ResolveHeaders(Request);
    }

    [HttpGet]
    public RemoteOfficeModel Get()
    {
      RemoteOfficeModel model = new RemoteOfficeModel();
      try
      {
        bool inserted = false;

        var dbObject = _context.RemoteOffice.FirstOrDefault();
        if (dbObject == null){
            dbObject = new RemoteOffice();
            _context.RemoteOffice.Add(dbObject);
            inserted = true;
        }

        dbObject.MapTo(model);

        if (inserted){
            _context.SaveChanges();
        }
      }
      catch
      {

      }

      return model;
    }

    [Authorize(Policy = "WebUser")]
    [HttpPost]
    public BusinessResult Post(RemoteOfficeModel model)
    {
      BusinessResult result = new BusinessResult();

      try
      {
        var dbObj = _context.RemoteOffice.FirstOrDefault(d => d.Id > 0);
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

  }
}