using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoneyBox.Api.Controllers
{
    public class IndexController : ApiController
    {
        [AllowAnonymous]
        [Route("")]
        public HttpResponseMessage GetIndex()
        {
            //var response = Request.CreateResponse(HttpStatusCode.Moved);
            //string fullyQualifiedUrl = Request.RequestUri.GetLeftPart(UriPartial.Authority);
            //response.Headers.Location = new Uri(fullyQualifiedUrl + "/swagger");
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
