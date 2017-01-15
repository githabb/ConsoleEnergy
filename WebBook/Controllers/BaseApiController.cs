using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebBook.Controllers
{
    public class BaseApiController : ApiController
    {
        protected IHttpActionResult Conflict(string message)
        {
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Conflict, message));
        }

        protected IHttpActionResult Conflict(string message, Exception exception)
        {
            return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.Conflict, message
#if DEBUG
                        , exception
#endif
            ));
        }
    }
}