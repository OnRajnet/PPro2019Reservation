using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Rapap.Areas.Admin.Controllers
{
    public class KartonazController : Controller
    {
        // GET: Admin/Kartonaz
        public ActionResult Index(int? page)
        {
            int itemsOnPage = 45;
            int pg = page.HasValue ? page.Value : 1;
            int totalKartony;

            KartonazDao kartonazDao = new KartonazDao();
            IList<Kartonaz> kartony = kartonazDao.GetKartonyLists(itemsOnPage, pg, out totalKartony);

            ViewBag.Pages = (int)Math.Ceiling((double)totalKartony / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;

            RapapUser user = new RapapUserDao().GetByLogin(User.Identity.Name);

            if (user.Role.Identifikator == "zakaznik")
                return View("IndexZakaznik", kartony);

            return View(kartony);
        }

        public ActionResult Search(string phrase)
        {
            KartonazDao kartonazDao = new KartonazDao();
            IList<Kartonaz> kartony = kartonazDao.SearchKartonaz(phrase);

            return View("IndexZakaznik", kartony);
        }

        [HttpPost]
        public ActionResult Reserve(FormCollection data)
        {
            KartonazDao kartonazDao = new KartonazDao();
            RezervaceDao rezervaceDao = new RezervaceDao();
            RapapUser user = new RapapUserDao().GetByLogin(User.Identity.Name);

            foreach (string key in data.AllKeys)
            {
                try
                {
                    if (data[key] != "false")
                    {
                        int id = int.Parse(key.Substring("chbxIsSelected".Length));
                        Kartonaz kartonaz = kartonazDao.GetById(id);

                        rezervaceDao.Create(new Rezervace()
                        {
                            Datum = DateTime.UtcNow,
                            Kartonaz = kartonaz,
                            Lepenka = null,
                            User = user
                        });
                        TempData["message-success"] = "Položka byla úspěšně rezervována";
                    }
                }
                catch (Exception)
                {
                    throw new HttpUnhandledException();
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateReserve()
        {
            return View();
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Add(Kartonaz karton)
        {
            if (ModelState.IsValid)
            {
                KartonazDao kartonazDao = new KartonazDao();
                kartonazDao.Create(karton);

                TempData["message-success"] = "Položka byla úspěšně přidána";
            }
            else
            {
                return View("Create", karton);
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            KartonazDao kartonazDao = new KartonazDao();
            Kartonaz k = kartonazDao.GetById(id);
            return View(k);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Update(Kartonaz karton)
        {
            try
            {
                KartonazDao kartonazDao = new KartonazDao();
                kartonazDao.Update(karton);
                TempData["message-success"] = "Kartonáž číslo: " + karton.Id + " , " + karton.Oznaceni + " , " + " Rozměr " + karton.Rozmer + " byla upravena";
            }
            catch (Exception)
            {
                throw;
            }
            return RedirectToAction("Index", "Kartonaz");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                KartonazDao kartonyDao = new KartonazDao();
                Kartonaz kartonaz = kartonyDao.GetById(id);

                kartonyDao.Delete(kartonaz);

                TempData["message-success"] = "Kartonáž" + kartonaz.Oznaceni + " Byla smazána";
            }
            catch (Exception exception)
            {
                throw;
            }

            return RedirectToAction("Index");
        }



    }
}