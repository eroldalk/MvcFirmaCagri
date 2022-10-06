using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcFirmaCagri.Models.entity;

namespace MvcFirmaCagri.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        DbisTakipEntities db = new DbisTakipEntities(); 
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(TBLFirmalar p)
        {
            var bilgiler = db.TBLFirmalar.FirstOrDefault(x=>x.Mail == p.Mail && x.Sifre == p.Sifre);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.Mail, false);
                Session["Mail"] = bilgiler.Mail.ToString();
                return RedirectToAction("Index","Default");//index yerine aktif çağrılar sayfası
            }
            else
            {
                return RedirectToAction("AktifCagrilar"); //aktif yerine index yazıldı
            }

            return View();
        }

    }
}