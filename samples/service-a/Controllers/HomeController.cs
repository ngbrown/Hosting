using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using service_a.Models;

namespace service_a.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> log;

        public HomeController(ILogger<HomeController> log)
        {
            this.log = log;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> About()
        {
            try
            {
                HttpClient client = new HttpClient();
                var result = await client.GetAsync("http://localhost:50572/");
                if (result.IsSuccessStatusCode)
                {
                    var responseString = await result.Content.ReadAsStringAsync();
                    log.LogInformation("Received {message}", responseString);
                    ViewData["Message"] = responseString;
                }
                else
                {
                    log.LogWarning("Request encountered error {reason}", result.ReasonPhrase);
                    ViewData["Message"] = "some error happened";
                }
            }
            catch (Exception ex)
            {
                log.LogWarning("Request encountered exception {exception}", ex);
                ViewData["Message"] = "exception happened";
            }

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
