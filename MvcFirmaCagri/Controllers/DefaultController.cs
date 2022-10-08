using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcFirmaCagri.Models.entity;

namespace MvcFirmaCagri.Controllers
{ 
    
    [Authorize]


    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            var mail = (string)Session["Mail"];
            var id = db.TBLFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var toplamcagri = db.TBLCagrilar.Where(x=>x.CagriFirma == id).Count();
            var aktifcagri = db.TBLCagrilar.Where(x => x.CagriFirma == id && x.Durum == true).Count();
            var pasifcagri = db.TBLCagrilar.Where(x => x.CagriFirma == id && x.Durum == false).Count();
            var yetkili = db.TBLFirmalar.Where(x => x.ID == id).Select(y => y.Yetkili).FirstOrDefault();
            var sektor = db.TBLFirmalar.Where(x => x.ID == id).Select(y => y.Sektor).FirstOrDefault();
            ViewBag.c1 = toplamcagri;
            ViewBag.c2 = aktifcagri;
            ViewBag.c3 = pasifcagri;
            ViewBag.c4 = yetkili;
            ViewBag.c5 = sektor;
            return View();
        }
        DbisTakipEntities db = new DbisTakipEntities();

       

        public ActionResult AktifCagrilar()
        {
            var mail = (string)Session["Mail"];
            var id=db.TBLFirmalar.Where(x=>x.Mail == mail).Select(y=>y.ID).FirstOrDefault();    
            var cagrilar = db.TBLCagrilar.Where(x=>x.Durum==true &&
            x.CagriFirma == id).ToList();
            return View(cagrilar);
        }
        public ActionResult PasifCagrilar()
        {
            var mail = (string)Session["Mail"];
            var id = db.TBLFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
            var cagrilar = db.TBLCagrilar.Where(x => x.Durum == false &&
            x.CagriFirma == id).ToList();
            return View(cagrilar);
        }
        [HttpGet]
        public ActionResult YeniCagri()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult YeniCagri(TBLCagrilar p)
        {

          var mail = (string)Session["Mail"];
          var id = db.TBLFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();
          p.Durum = true;
          p.Tarih= DateTime.Parse(DateTime.Now.ToShortDateString());
          p.CagriFirma = id;
          db.TBLCagrilar.Add(p);
          db.SaveChanges();
          return RedirectToAction("AktifCagrilar");
        }
        public ActionResult CagriDetay(int id)
        {
            var cagri = db.TBLCagriDetay.Where(x => x.Cagri == id).ToList();
            return View(cagri);
        }

        public ActionResult CagriGetir(int id)
        {
            var cagri = db.TBLCagrilar.Find(id);
            return View("CagriGetir", cagri);
        }
        public ActionResult CagriDuzenle(TBLCagrilar p)
        {
            var cagri = db.TBLCagrilar.Find(p.ID);
            cagri.Konu= p.Konu;
            cagri.Aciklama = p.Aciklama;
            db.SaveChanges();
            return RedirectToAction("AktifCagrilar");
        }
        public ActionResult ProfilDuzenle()
        {
            var mail = (string)Session["Mail"];
            var id = db.TBLFirmalar.Where(x => x.Mail == mail).Select(y => y.ID).FirstOrDefault();

            var profil = db.TBLFirmalar.Where(x => x.ID == id).ToList().FirstOrDefault();
            return View(profil);
        }

        public PartialViewResult partial1()
        {
            var mail = (string)Session["Mail"];
            var mesajlar = db.TBLMesajlar.Where(x => x.Alici == 1 && x.Durum == true).ToList();
            var mesajsayisi = db.TBLMesajlar.Where(x => x.Alici == 1 && x.Durum == true).Count();
            ViewBag.m1 = mesajsayisi;
            return PartialView(mesajlar);
        }

        public PartialViewResult partial2()
        {
            var mail = (string)Session["Mail"];
            var id = db.TBLFirmalar.Where(x => x.Mail == mail).Select(y=> y.ID).FirstOrDefault();
            var cagrilar = db.TBLCagrilar.Where(x => x.CagriFirma == id && x.Durum == true).ToList();

            var cagrisayisi = db.TBLCagrilar.Where(x => x.CagriFirma == id && x.Durum == true).Count();
            ViewBag.a1 = cagrisayisi;

            return PartialView(cagrilar);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            
            return RedirectToAction("Index", "Login");
        }









    }
}