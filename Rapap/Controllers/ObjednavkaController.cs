using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Rapap.Controllers
{
    public class ObjednavkaController : Controller
    {
        // GET: Objednavka
        public ActionResult Index()
        {
            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            IList<Objednavka> objednavky = objednavkaDao.GetAll();

            return View(objednavky);

        }
    }
}