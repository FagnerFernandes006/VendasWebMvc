using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Models;
using VendasWebMvc.Models.ViewModels;
using VendasWebMvc.Services;

namespace VendasWebMvc.Controllers
{
    public class RegistroVendasController : Controller
    {
        private readonly RegistroVendasService _registroVendasService;
        private readonly VendedorService _vendedorService;

        public RegistroVendasController(RegistroVendasService registroVendasService, VendedorService vendedorService)
        {
            _registroVendasService = registroVendasService;
            _vendedorService = vendedorService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> BuscaSimples(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _registroVendasService.FindByDateAsync(minDate, maxDate);
            return View(result);
        }
        public async Task<IActionResult> BuscaAgrupada(DateTime? minDate, DateTime? maxDate)
        {
            if (!minDate.HasValue)
            {
                minDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            if (!maxDate.HasValue)
            {
                maxDate = new DateTime(DateTime.Now.Year, 1, 1);
            }
            ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
            ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");
            var result = await _registroVendasService.FindByDateGroupAsync(minDate, maxDate);
            return View(result);
        }
        public async Task<IActionResult> Criar()
        {
            var vendedores = await _vendedorService.FindAllAsync();
            var viewModel = new RegistroVendasFormViewModel { Vendedores = vendedores };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(RegistroVendas registroVendas)
        {
            if (!ModelState.IsValid) // Teste de validação
            {
                var vendedores = await _vendedorService.FindAllAsync();
                var viewModel = new RegistroVendasFormViewModel {RegistroVendas = registroVendas, Vendedores = vendedores };
                return View(viewModel);
            }
            await _registroVendasService.InserirAsync(registroVendas);
            return RedirectToAction(nameof(Index));
        }
    }
}
