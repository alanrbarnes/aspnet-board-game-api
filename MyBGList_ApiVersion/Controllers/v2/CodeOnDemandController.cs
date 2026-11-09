using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace MyBGList_ApiVersion.Controllers.v2
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [EnableCors("AnyOrigin_GetOnly")]
    [ApiVersion("2.0")]
    public class CodeOnDemandController : ControllerBase
    {

        [HttpGet("Test")]
        [ResponseCache(NoStore = true)]
        public ContentResult Test()
        {
            string html = "<script>" +
                "window.alert('Your client supports JavaScript!" +
                "\\r\\n\\r\\n" +
                $"Server time (UTC): {DateTime.UtcNow.ToString("o")}" +
                "\\r\\n" +
                "Client time (UTC): ' + new Date().toISOString());" +
                "</script>" +
                "<noscript>Your client does not support JavaScript</noscript>";

            return Content(html, "text/html", Encoding.UTF8);
        }

        [HttpGet("Test2")]
        [ResponseCache(NoStore = true)]
        public ContentResult Test2(int? addMinutes)
        {
            DateTime dt = DateTime.UtcNow.AddMinutes(addMinutes ?? 0);

            string html = "<script>" +
                "window.alert('Your client supports JavaScript!" +
                "\\r\\n\\r\\n" +
                $"Server time (UTC): {dt.ToString("o") + addMinutes}" +
                "\\r\\n" +
                "Client time (UTC): ' + new Date().toISOString());" +
                "</script>" +
                "<noscript>Your client does not support JavaScript</noscript>";

            return Content(html, "text/html", Encoding.UTF8);
        }
    }
}
