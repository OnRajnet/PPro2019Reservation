using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Rapap.Controllers
{
    public class PoptavkaController : Controller
    {
        // GET: Poptavka
        public ActionResult Index()
        {
            PoptavkaDao poptavkaDao = new PoptavkaDao();
            IList<Poptavka> poptavka = poptavkaDao.GetAll();

            return View(poptavka);
        }

        public ActionResult Create()
        {

            OblastDao oblastDao = new OblastDao();
            IList<Oblast> oblast = oblastDao.GetAll();
            ViewBag.Typ = oblast;

            return View();
        }

        [HttpPost]
        public ActionResult Add(Poptavka poptavka, int oblastId)
        {
            if (ModelState.IsValid)
            {
                OblastDao oblastDao = new OblastDao();
                Oblast oblast = oblastDao.GetById(oblastId);

                poptavka.Typ = oblast;

                PoptavkaDao poptavkaDao = new PoptavkaDao();
                poptavkaDao.Create(poptavka);

                TempData["message-success"] = "Poptávka byla úspěšně odeslána";

            }
            else
            {
                ViewBag.Typ = new OblastDao().GetAll();
                return View("Create", poptavka);
            }

            return RedirectToAction("Index");
        }
    }
}