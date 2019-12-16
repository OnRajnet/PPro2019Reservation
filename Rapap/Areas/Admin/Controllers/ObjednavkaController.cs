using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Rapap.Areas.Admin.Controllers
{
    public class ObjednavkaController : Controller
    {
        public ActionResult Index(int? page)
        {

            int itemsOnPage = 45;
            int pg = page.HasValue ? page.Value : 1;
            int totalObjednavky;



            ObjednavkaDao objednavkaDao = new ObjednavkaDao();
            IList<Objednavka> objednavky = objednavkaDao.GetLObjednavkyLists(itemsOnPage, pg, out totalObjednavky);

            ViewBag.Pages = (int)Math.Ceiling((double)totalObjednavky / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;

            RapapUser user = new RapapUserDao().GetByLogin(User.Identity.Name);

            if (user.Role.Identifikator == "zakaznik")
                return View("IndexZakaznik", objednavky.Where(x => x.User.Id == user.Id).ToList());


            return View(objednavky);


        }



    }
}