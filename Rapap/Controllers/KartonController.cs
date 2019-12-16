using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Rapap.Controllers
{
    public class KartonController : Controller
    {
        // GET: Karton
        public ActionResult Index()
        {
            KartonazDao kartonazDao = new KartonazDao();
            IList<Kartonaz> karton = kartonazDao.GetAll();

            return View(karton);
        }
    }
}