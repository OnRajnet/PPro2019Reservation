using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Model;
using DataAccess.Dao;

namespace Rapap.Controllers
{

    public class LepenkaController : Controller
    {
        // GET: Lepenka
        public ActionResult Index()
        {

            LepenkaDao lepenkaDao = new LepenkaDao();
            IList<Lepenka> lepenky = lepenkaDao.GetAll();

            return View(lepenky);



        }
    }

}