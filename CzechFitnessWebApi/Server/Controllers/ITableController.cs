using Microsoft.AspNetCore.Mvc;

namespace IPISserver.Controllers
{
    interface ITableController<T>
    {        
        public ActionResult GetAll();
        public ActionResult GetSingle(int id);
        public ActionResult Put([FromBody] T model);
        public ActionResult Post([FromBody] T model);
        public ActionResult Delete(int id);
    }
}