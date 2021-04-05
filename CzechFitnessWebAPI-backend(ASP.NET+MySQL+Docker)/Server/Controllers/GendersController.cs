using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using IPISserver.Handlers;
using IPISserver.Models;

namespace IPISserver.Controllers
{
    [ApiController]
    [Route("genders/")]
    public class GendersController : ControllerBase
    {
        private GendersHandler handler = new GendersHandler();

        /// <summary>
        /// Select all rows from `Genders` table (GET)
        /// </summary>
        [HttpGet]
        public ActionResult GetAll()
        {
            string? result = handler.Select();
            return (result==null)? NoContent() : Ok(result);
        }

        /// <summary>
        /// Select single row from `Genders` table by specific id (GET)
        /// </summary>
        /// <param name="id">select row id</param>
        [HttpGet("{id}")]
        public ActionResult GetSingle(int id) 
        { 
            string? result = handler.Select(id);
            return (result==null)? NoContent() : Ok(result);
        }


        /// <summary>
        /// Insert row in `Genders` table by special body (POST)
        /// </summary>
        /// <param name="model">model of `Genders` table. Fill automatically from body JSON</param>
        [HttpPut]
        public ActionResult Put([FromBody] GendersModel model)
        {
            string? result = handler.InsertNewRow(model);
            return (result == null) ? Ok() : NotFound(result);
        }

        /// <summary>
        /// Update row in `Genders` table by special body (POST)
        /// </summary>
        /// <param name="model">model of `Genders` table. Fill automatically from body JSON</param>
        [HttpPost]
        public ActionResult Post([FromBody] GendersModel model)
        {
            string? result = handler.UpdateRow(model);
            return (result==null)? Ok() : NotFound(result);
        }

        /// <summary>
        /// Delete row in `Genders` table by specific id (DELETE)
        /// </summary>
        /// <param name="id">Remove row id</param>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) 
        {
            string? result = handler.DeleteRow(id);
            return (result==null)? Ok() : NotFound(result);
        }
    }
}
