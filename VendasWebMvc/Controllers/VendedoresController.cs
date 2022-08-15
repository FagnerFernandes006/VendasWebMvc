using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using VendasWebMvc.Services;

namespace VendasWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;

        public VendedoresController(VendedorService vendedorService)
        {
            _vendedorService = vendedorService;
        }
        public IActionResult Index()
        {
            var list = _vendedorService.FindAll();
            return View(list);
        }
        public IActionResult Criar()
        {
            return View();
        }   

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Criar(Vendedor vendedor)
        {
            _vendedorService.Inserir(vendedor);
            return RedirectToAction(nameof(Index));
        }
    }
}
