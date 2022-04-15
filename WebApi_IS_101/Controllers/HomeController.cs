using Microsoft.AspNetCore.Mvc;
using System;
using UAParser;
using WebApi_IS_101.RequestModels;

namespace WebApi_IS_101.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            var userAgent = Request.Headers["User-Agent"];

            // sources:
            // https://github.com/ua-parser/uap-csharp
            // https://stackoverflow.com/a/52609204
            ClientInfo ci = Parser.GetDefault().Parse(userAgent.ToString());

            var formatted = $"{ci.UA} {ci.OS}";

            var response = System.Text.Json.JsonSerializer.Serialize(userAgent);
            response += "\n" + formatted;

            Console.WriteLine(response);

            return response;
        }

        [HttpPost]
        public int Post(PostRequest request)
        {
            return 0;
        }
    }
}
