using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rapap.Areas.Admin.Controllers
{
        public class LepenkyController : Controller
        {
            // GET: Lepenka
            public ActionResult Index(int? page)
            {

                int itemsOnPage = 45;
                int pg = page.HasValue ? page.Value : 1;
                int totalLepenky;



                LepenkaDao lepenkaDao = new LepenkaDao();
                IList<Lepenka> lepenky = lepenkaDao.GetLepenkyLists(itemsOnPage, pg, out totalLepenky);

                ViewBag.Pages = (int)Math.Ceiling((double) totalLepenky / (double)itemsOnPage);
                ViewBag.CurrentPage = pg;

                ViewBag.Kvality = new LepenkyKvalitaDao().GetAll();
                RapapUser user = new RapapUserDao().GetByLogin(User.Identity.Name);

                if (user.Role.Identifikator == "zakaznik")
                {
                    return View("IndexZakaznik", lepenky);
                }
                else
                {
                    return View(lepenky);
                }
            }

            public ActionResult Search(string phrase)
            {
                LepenkaDao lepenkadao = new LepenkaDao();
                IList<Lepenka> lepenky = lepenkadao.SearchLepenky(phrase);

                return View("IndexZakaznik", lepenky);
            }
            
            [HttpPost]
            public ActionResult Reserve(FormCollection data)
            {
                LepenkaDao lepenkaDao = new LepenkaDao();
                RezervaceDao rezervaceDao = new RezervaceDao();
                RapapUser user = new RapapUserDao().GetByLogin(User.Identity.Name);

                foreach (string key in data.AllKeys)
                {
                    try
                    {
                        if (data[key] != "false")
                        {
                            int id = int.Parse(key.Substring("chbxIsSelected".Length));
                            Lepenka lepenka = lepenkaDao.GetById(id);

                            rezervaceDao.Create(new Rezervace()
                            {
                                Datum = DateTime.UtcNow,
                                Kartonaz = null,
                                Lepenka = lepenka,
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

            public ActionResult Kvalita(int kvalitaId)
            {
                IList<Lepenka> lepenky = new LepenkaDao().GetLepenkyInKvalitaId(kvalitaId);
                ViewBag.Kvality = new LepenkyKvalitaDao().GetAll();

                return View("IndexZakaznik", lepenky);
            }

            [Authorize(Roles = "Admin")]
            public ActionResult Create()
            {
                LepenkyKvalitaDao lepenkyKvalitaDao = new LepenkyKvalitaDao();
                IList<LepenkaKvalita> kvalita = lepenkyKvalitaDao.GetAll();
                ViewBag.Kvalita = kvalita;

                return View();
            }
            [Authorize(Roles = "Admin")]
            [HttpPost]
            public ActionResult Add(Lepenka lepenka, int kvalitaId)
            {
                if (ModelState.IsValid)
                {
                    LepenkyKvalitaDao lepenkyKvalitaDao = new LepenkyKvalitaDao();
                    LepenkaKvalita lepenkaKvalita = lepenkyKvalitaDao.GetById(kvalitaId);

                    lepenka.Kvalita = lepenkaKvalita;

                    LepenkaDao lepenkaDao = new LepenkaDao();
                    lepenkaDao.Create(lepenka);

                    TempData["message-success"] = "Lepenka byla uspesne pridana";
                }
                else {
                    return View("Create", lepenka);
                }

                return RedirectToAction("Index");
            }
            [Authorize(Roles = "Admin")]
            public ActionResult Edit(int id)
            {
                LepenkaDao lepenkaDao = new LepenkaDao();
                LepenkyKvalitaDao lepenkaKvalitaDao = new LepenkyKvalitaDao();

                Lepenka l = lepenkaDao.GetById(id);
                ViewBag.Kvalita = lepenkaKvalitaDao.GetAll();

                return View(l);
            }
            [Authorize(Roles = "Admin")]
            [HttpPost]
            public ActionResult Update(Lepenka lepenka, int kvalitaId)
            {
                try
                {
                    LepenkaDao lepenkaDao = new LepenkaDao();
                    LepenkyKvalitaDao lepenkyKvalitaDao = new LepenkyKvalitaDao();

                    LepenkaKvalita lepenkaKvalita = lepenkyKvalitaDao.GetById(kvalitaId);

                    lepenka.Kvalita = lepenkaKvalita;

                    lepenkaDao.Update(lepenka);
                    TempData["message-success"] = "Lepenka číslo: " + lepenka.Id + " , " + lepenka.Nazev + " , " + " Rozměr " + lepenka.Rozmer + " byla upravena";
                }
                catch (Exception)
                {
                    throw;
                }

                return RedirectToAction("Index", "Lepenky");
            }

            [Authorize(Roles = "Admin")]
            public ActionResult Delete(int id)
            {
                try
                {
                    LepenkaDao lepenkaDao = new LepenkaDao();
                    Lepenka lepenka = lepenkaDao.GetById(id);

                    lepenkaDao.Delete(lepenka);

                    TempData["message-success"] = "Lepenka " + lepenka.Nazev + " Byla smazána";
                }
                catch (Exception exception)
                {
                    throw;
                }

                return RedirectToAction("Index");
            }
    }
}