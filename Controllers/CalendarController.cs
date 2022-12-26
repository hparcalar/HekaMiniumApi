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
    public class CalendarController : HekaControllerBase{
        public CalendarController(HekaMiniumSchema context): base(context){ 
            ResolveHeaders(Request);
        }

        [HttpGet]
        public IEnumerable<CalendarElementModel> Get()
        {
            CalendarElementModel[] data = new CalendarElementModel[0];
            try
            {
                data = _context.CalendarElement
                .Select(d => new CalendarElementModel{
                    Id = d.Id,
                    CalendarId = d.CalendarId,
                    Category = d.Category,
                    DragBgColor = d.DragBgColor,
                    End = d.End,
                    IsAllDay = d.IsAllDay,
                    IsImportant = d.IsImportant,
                    IsPrivate = d.IsPrivate,
                    IsReadOnly = d.IsReadOnly,
                    Location = d.Location,
                    Start = d.Start,
                    Title = d.Title,
                    Body = d.Body,
                    State = d.State,
                }).ToArray();
            }
            catch
            {
                
            }
            
            return data;
        }


        [HttpGet]
        [Route("{id}")]
        public CalendarElementModel Get(int id)
        {
            CalendarElementModel data = new CalendarElementModel();
            try
            {
                data = _context.CalendarElement.Where(d => d.Id == id).Select(d => new CalendarElementModel{
                        Id = d.Id,
                        CalendarId = d.CalendarId,
                        Category = d.Category,
                        DragBgColor = d.DragBgColor,
                        End = d.End,
                        IsAllDay = d.IsAllDay,
                        IsImportant = d.IsImportant,
                        IsPrivate = d.IsPrivate,
                        IsReadOnly = d.IsReadOnly,
                        Location = d.Location,
                        Start = d.Start,
                        Title = d.Title,
                        Body = d.Body,
                        State = d.State,
                    }).FirstOrDefault();
            }
            catch
            {
                
            }
            
            return data;
        }


        [Authorize(Policy = "WebUser")]
        [HttpPost]
        public BusinessResult Post(CalendarElementModel model){
            BusinessResult result = new BusinessResult();

            try
            {
                var dbObj = _context.CalendarElement.FirstOrDefault(d => d.Id == model.Id);
                if (dbObj == null){
                    dbObj = new CalendarElement();
                    _context.CalendarElement.Add(dbObj);
                }

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
                var dbObj = _context.CalendarElement.FirstOrDefault(d => d.Id == id);
                if (dbObj == null)
                    throw new Exception("");

                _context.CalendarElement.Remove(dbObj);

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