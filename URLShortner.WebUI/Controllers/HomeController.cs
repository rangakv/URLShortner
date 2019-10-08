using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using URLShortner.Business;
using URLShortner.ServiceRepository;

namespace URLShortner.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.ShortenedUrl = string.Empty;
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            string longUrl = Convert.ToString(form["longUrl"]);
            try
            {
                ServiceHandler serviceObj = new ServiceHandler();
                HttpResponseMessage response = serviceObj.GetResponse("api/shorturl");
                response.EnsureSuccessStatusCode();
                var resp = response.Content.ReadAsAsync<URLResponse>().Result;
                ViewBag.ShortenedUrl = resp.id;
                return View();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet]
        public ActionResult HashUrl(string sUrl)
        {
            try
            {
                ServiceHandler serviceObj = new ServiceHandler();
                HttpResponseMessage response = serviceObj.GetResponse("api/hashurl?sUrl=" + sUrl);
                response.EnsureSuccessStatusCode();
                var resp = response.Content.ReadAsAsync<URLResponse>().Result;
                ViewBag.ShortenedUrl = resp.hashurl;
                return Json(new
                {
                    msg = String.Format("{0}", resp.hashurl)
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult HealthCheck(FormCollection form)
        {
            try
            {
                ServiceHandler serviceObj = new ServiceHandler();
                HttpResponseMessage response = serviceObj.GetResponse("api/healthcheck");
                response.EnsureSuccessStatusCode();
                var resp = response.Content.ReadAsAsync<string>().Result;
                ViewBag.ShortenedUrl = resp;
                return View("Index");
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}