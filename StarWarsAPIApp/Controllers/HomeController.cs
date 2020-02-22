using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StarWarsAPIApp.Models;
using System.Net.Http;
using System.Text.Json;
namespace StarWarsAPIApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<Character> GetCharacter(int num)
        {
            Character c = new Character();
            using (var httpclient = new HttpClient())
            {
                using(var response = await httpclient.GetAsync($"https://swapi.co/api/people/{num}/"))
                {
                    var sr = await response.Content.ReadAsStringAsync();
                    JsonDocument jdoc = JsonDocument.Parse(sr);
                  
                    c = JsonSerializer.Deserialize<Character>(sr);
                    
                    
                    
                }
            }
            var p = await GetPlanet(c.homeworld);
            c.homeworld = p.name;
            return c;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ShowCharacter(int num)
        {
             var c = await GetCharacter(num);

            //var s = await GetSpecies(c.spec);
            //c.spec = s.name;
            return View(c);

        }

        public async Task<Planet> GetPlanet(string page)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{page}"))
                {
                    var sr = await response.Content.ReadAsStringAsync();

                    Planet p = JsonSerializer.Deserialize<Planet>(sr);
                    return p;
                }
            }
        }
        public async Task<Species> GetSpecies(string page)
        {
            using (var httpclient = new HttpClient())
            {
                using (var response = await httpclient.GetAsync($"{page}"))
                {
                    var sr = await response.Content.ReadAsStringAsync();
                    Species sp = JsonSerializer.Deserialize<Species>(sr);
                    return sp;
                }
            }
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
