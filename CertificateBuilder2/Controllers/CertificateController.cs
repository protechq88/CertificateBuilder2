using System.Web.Mvc;
using CertificateBuilder2.Models;
using System.Collections.Generic;
using System.Web.UI;
using System.Xml;
using Newtonsoft.Json;
using System;

namespace CertificateBuilder2.Controllers
{
    public class CertificateController : Controller
    {
        public ActionResult Certificate(int CertId)
        {
            CertificateModel _certificate = CertificateUtility.GetCertificate(CertId);
            return View(_certificate);
        }

        [HttpPost]
        public ActionResult SaveAndContinue(CertificateModel Certificate)
        {
            int CertId = CertificateUtility.SaveAndContinue(Certificate);
            return Json(CertId, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SaveAndExit(CertificateModel Certificate)
        {
            CertificateUtility.SaveAndExit(Certificate);

            return Content("Success");
        }

        public ActionResult Index()
        {
            return View();
        }

        [OutputCache(Location = OutputCacheLocation.None)]
        public ActionResult GetCertificateList()
        {
            var results = CertificateUtility.GetCertList(1);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteCertificate(int CertId)
        {
            CertificateUtility.DeleteCertificate(CertId);

            var results = CertificateUtility.GetCertList(1);
            return Json(results, JsonRequestBehavior.AllowGet);
        }

    }
}