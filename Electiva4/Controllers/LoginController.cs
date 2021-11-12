using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Electiva4.Models;
using System.Web.Mvc;
namespace Electiva4.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(Login login )
        {
            return View();
        }
    }
}