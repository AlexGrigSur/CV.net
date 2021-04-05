using Microsoft.AspNetCore.Mvc;
using IPISserver.Handlers;
using IPISserver.Models;
using System;
using Newtonsoft.Json;

namespace IPISserver.Controllers
{
    [ApiController]
    [Route("people/")]
    public class PeopleController : ControllerBase
    {
        private PeopleHandler handler = new PeopleHandler();

        /// <summary>
        /// Select all rows from `People` table (GET)
        /// </summary>
        [HttpGet]
        public ActionResult GetAll()
        {
            string? result = handler.Select();
            return (result==null)? NoContent() : Ok(result);
        }

        /// <summary>
        /// Select single row from `People` table by specific id (GET)
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
        /// <param name="model">model of `People` table. Fill automatically from body JSON</param>
        [HttpPut]
        public ActionResult Put([FromBody] PeopleModel model)
        {
            string? result = handler.InsertNewRow(model);
            return (result == null) ? Ok() : NotFound(result);
        }


        /// <summary>
        /// Update row in `People` table by special body (POST)
        /// </summary>
        /// <param name="model">model of `People` table. Fill automatically from body JSON</param>
        [HttpPost]
        public ActionResult Post([FromBody] PeopleModel model) 
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