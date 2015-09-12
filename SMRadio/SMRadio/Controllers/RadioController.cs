using SM.Radio.DAL;
using SM.Radio.Entity;
using SM.Radio.IBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SM.Radio.Web.Controllers
{
    public class RadioController : BaseController
    {
        //
        // GET: /Radio/

        private readonly IRadioBLL radioBLL;

        public RadioController(IRadioBLL radioBLL)
        {
            this.radioBLL = radioBLL;
        }

        public ActionResult Index()
        {
            return View(radioBLL.GetAll<Radios>());
        }

        public JsonResult CreateRadio(Radios radio)
        {
            
            //db.radios.Add(radio);
            //db.SaveChanges();
            return Json(new { 
                Status = true
            });
        }

    }
}
