using System;
using System.Collections.Generic;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationTest.Models;

namespace WebApplicationTest.Controllers
{
    public class UserRegController : Controller
    {
        // GET: UserReg
        
        public ActionResult Index()
        {
            UserReg userReg = new UserReg();
           
            List<UserReg> ousers = new List<UserReg>();
            ousers= userReg.gets();
            return View(ousers);
        }
       
        public ActionResult FirstAjax(UserReg us)
        {
            UserReg userReg = new UserReg();
        UserReg userss = userReg.save(us);
            return Json(userss, JsonRequestBehavior.AllowGet);
        }
        public ActionResult delete(UserReg us)
        {
            UserReg userReg = new UserReg();
             userReg.delete(us);
            return Json("done", JsonRequestBehavior.AllowGet);
        }

    }
}