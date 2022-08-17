using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models.ViewModels;

namespace VendasWebMvc.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Vendas Web Mvc";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Dados";
            ViewData["Telefone"] = "Telefone: (DD)99999-9999";
            ViewData["Email"] = "Email: fagner.b.fernandes@gmail.com";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new VendasWebMvc.Models.ViewModels.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
