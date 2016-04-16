using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting.Web.Http;

namespace WebAPiFllowUp.Controllers
{
    public class AboutController : ApiController
    {
        [POST("about")]
        public string about()
        {
            var test = new
            {
                name = "xpy0928",
                gender = "男",
                age = 12
            };
            return Newtonsoft.Json.JsonConvert.SerializeObject(test);
        }
    }
}
