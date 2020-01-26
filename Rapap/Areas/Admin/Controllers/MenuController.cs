using DataAccess.Dao;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Rapap.Areas.Admin.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        [ChildActionOnly]
        // GET: Admin/Menu
        public ActionResult Index()
        {
            RapapUserDao rapapUserDao = new RapapUserDao();
            RapapUser rapapUser = rapapUserDao.GetByLogin(User.Identity.Name);

            return View(rapapUser);
        }
    }
}