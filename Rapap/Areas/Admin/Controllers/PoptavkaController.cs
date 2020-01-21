using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Rapap.Areas.Admin.Controllers
{
    public class PoptavkaController : Controller
    {
        public ActionResult Index(int? page, string druh)
        {
            int itemsOnPage = 45;
            int pg = page.HasValue ? page.Value : 1;
            int totalPoptavky;

            PoptavkaDao poptavkaDao = new PoptavkaDao();
            IList<Poptavka> poptavky = poptavkaDao.GetPoptavkyLists(itemsOnPage, pg, druh, out totalPoptavky);

            ViewBag.Pages = (int)Math.Ceiling((double)totalPoptavky / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;

            ViewBag.Typ = new OblastDao().GetAll();
            RapapUser user = new RapapUserDao().GetByLogin(User.Identity.Name);

            if (user.Role.Identifikator == "zakaznik")
                return View("IndexZakaznik", poptavky);

            return View(poptavky);
        }

        public ActionResult Search(string phrase)
        {
            PoptavkaDao poptavkaDao = new PoptavkaDao();
            IList<Poptavka> poptavky = poptavkaDao.SearchPoptavky(phrase);

            return View("IndexZakaznik", poptavky);
        }

        public ActionResult Oblast(int oblastId)
        {
            IList<Poptavka> poptavky = new PoptavkaDao().GetPoptavkyInOblastId(oblastId);
            ViewBag.Typ = new OblastDao().GetAll();

            return View("IndexZakaznik", poptavky);
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
                return View("Create", poptavka);
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {

            try
            {

                PoptavkaDao poptavkaDao = new PoptavkaDao();
                Poptavka poptavka = poptavkaDao.GetById(id);

                poptavkaDao.Delete(poptavka);

                TempData["message-success"] = "Poptávka " + poptavka.Id + " Byla smazána";
            }
            catch (Exception exception)
            {
                throw;
            }

            return RedirectToAction("Index");
        }
    }


}