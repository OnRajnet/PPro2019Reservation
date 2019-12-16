using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Rapap.Areas.Admin.Controllers
{
    public class RezervaceController : Controller
    {
        // GET: Admin/Rezervace
        public ActionResult Index(int? page, string druh)
        {

            int itemsOnPage = 45;
            int pg = page.HasValue ? page.Value : 1;
            int totalPoptavky;



            RezervaceDao rezervaceDao = new RezervaceDao();
            IList<Rezervace> rezervace = rezervaceDao.GetRezervaceLists(itemsOnPage, pg, druh, out totalPoptavky);

            ViewBag.Pages = (int)Math.Ceiling((double)totalPoptavky / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;

            RapapUser user = new RapapUserDao().GetByLogin(User.Identity.Name);

            if (user.Role.Identifikator == "zakaznik")
                return View("IndexZakaznik", rezervace.Where(x => x.User.Id == user.Id).ToList());


            return View(rezervace);


        }
    }
}