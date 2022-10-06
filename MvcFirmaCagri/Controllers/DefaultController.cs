using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcFirmaCagri.Models.entity;

namespace MvcFirmaCagri.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        DbisTakipEntities db = new DbisTakipEntities();

        [Authorize]

        public ActionResult AktifCagrilar()
        {
            var cagrilar = db.TBLCagrilar.Where(x=>x.Durum==true &&
            x.CagriFirma == 12).ToList();
            return View(cagrilar);
        }
        public ActionResult PasifCagrilar()
        {
            var cagrilar = db.TBLCagrilar.Where(x => x.Durum == false &&
            x.CagriFirma == 12).ToList();
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
            p.Durum = true;
            p.Tarih= DateTime.Parse(DateTime.Now.ToShortDateString());
            p.CagriFirma = 12;
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
    }
}