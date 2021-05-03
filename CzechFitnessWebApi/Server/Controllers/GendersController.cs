using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using IPISserver.Handlers;
using IPISserver.Models;

namespace IPISserver.Controllers
{
    [ApiController]
    [Route("genders/")]
    public class GendersController<T> : ControllerBase, ITableController<T> where T: GendersModel
    {
        private GendersHandler handler = new GendersHandler();

        [HttpGet]
        public ActionResult GetAll()
        {
            string? result = handler.Select();
            return (result == null) ? NoContent() : Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult GetSingle(int id) 
        { 
            string? result = handler.Select(id);
            return (result==null)
                ? NoContent() 
                : Ok(result);
        }

        [HttpPut]
        public ActionResult Put([FromBody] T model)
        {
            string? result = handler.InsertNewRow(model);
            return Int32.TryParse(result, out _) 
                ? Ok(JsonConvert.SerializeObject(new { id=result})) 
                : NotFound(result);
        }

        [HttpPost]
        public ActionResult Post([FromBody] T model)
        {
            string? result = handler.UpdateRow(model);
            return (result==null)
                ? Ok() 
                : NotFound(result);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            string? result = handler.DeleteRow(id);
            return (result==null)
                ? Ok() 
                : NotFound(result);
        }
    }
}
