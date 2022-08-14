using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;

namespace VendasWebMvc.Controllers
{
    public class DepartamentosController : Controller
    {
        public IActionResult Index()
        {
            List<Departamento> lista = new List<Departamento>();
            lista.Add(new Departamento { Id = 1, Nome = "Estoque" });
            lista.Add(new Departamento { Id = 2, Nome = "Eletronicos" });
            lista.Add(new Departamento { Id = 3, Nome = "Assistência" });
            lista.Add(new Departamento { Id = 4, Nome = "Financeiro" });
            return View(lista);
        }
    }
}
