using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using URLShortner.Business;

namespace URLShortner.WebAPI.Controllers
{
    public class URLController : ApiController
    {
        [Route("api/shorturl")]
        [HttpPost]
        public HttpResponseMessage ShortenURL(string longUrl)
        {
            URLManager _urlManager = new URLManager();
            var result = _urlManager.Shorten(longUrl);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
        [Route("api/hashurl")]
        [HttpGet]
        public HttpResponseMessage HashUrl(string sUrl)
        {
            URLManager _urlManager = new URLManager();
            var result = _urlManager.HashUrl(sUrl);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
        [Route("api/healthcheck")]
        [HttpGet]
        public HttpResponseMessage HealthCheck()
        {
            string result = "Service is active";
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, result);
            return response;
        }
    }
}
