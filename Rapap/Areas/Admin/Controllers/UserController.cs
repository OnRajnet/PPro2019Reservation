using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Dao;
using DataAccess.Model;

namespace Rapap.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        // GET: Admin/User
        public ActionResult Index(int? page)
        {

            int itemsOnPage = 45;
            int pg = page.HasValue ? page.Value : 1;
            int totalUser;



            RapapUserDao userDao = new RapapUserDao();
            IList<RapapUser> users = userDao.GetUserLists(itemsOnPage, pg, out totalUser);

            ViewBag.Pages = (int)Math.Ceiling((double)totalUser / (double)itemsOnPage);
            ViewBag.CurrentPage = pg;

            ViewBag.Kvality = new RapapRoleDao().GetAll();

            return View(users);
        }


        public ActionResult Role(int roleId)
        {

            IList<RapapUser> user = new RapapUserDao().GetUserInRoleId(roleId);
            ViewBag.Kvality = new RapapRoleDao().GetAll();
            return View("index", user);

        }


    public ActionResult Create()
        {

            RapapRoleDao Roledao = new RapapRoleDao();
            IList<RapapRole> role = Roledao.GetAll();
            ViewBag.Kvalita = role;
            return View();
        }

        [HttpPost]
        public ActionResult Add(RapapUser user, int roleId)
        {

            if (ModelState.IsValid)
            {

                RapapRoleDao roleDao = new RapapRoleDao();
                RapapRole role = roleDao.GetById(roleId);

                user.Role = role;

                RapapUserDao userDao = new RapapUserDao();
                user.Password = userDao.Encrypt(user.Password);
                userDao.Create(user);

                TempData["message-success"] = "Uživatel přidán";

            }
            else
            {
                return View("Create", user); 
            }

            return RedirectToAction("Index");
        }
    }

}