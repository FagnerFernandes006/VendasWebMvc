using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using VendasWebMvc.Models.ViewModels;
using VendasWebMvc.Services;

namespace VendasWebMvc.Controllers
{
    public class EmpresasController : Controller
    {
        private readonly EmpresaService _empresaService;

        public EmpresasController(EmpresaService empresaService)
        {
            _empresaService = empresaService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _empresaService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Criar()
        {
            var empresas = await _empresaService.FindAllAsync();
            var viewModel = new EmpresaFormViewModel { };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Empresa empresa)
        {
            if (!ModelState.IsValid) // Teste de validação
            {
                var viewModel = new EmpresaFormViewModel { Empresa = empresa };
                return View(viewModel);
            }
            await _empresaService.InserirAsync(empresa);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não foi fornecido!" });
            }
            var obj = await _empresaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado!" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(Empresa empresa)
        {
            try
            {
                var vendedor = await _empresaService.FindByIdAsyncVendedor(empresa.Id);
                if (vendedor != null)
                {
                    return RedirectToAction(nameof(Erro), new { message = "Não é possível deletar esta empresa, pois pussui vendedores" });
                }
                await _empresaService.RemoverAsync(empresa.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Erro), new { message = "Não é possível deletar esta empresa" });
            }
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não foi fornecido!" });
            }
            var obj = await _empresaService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado!" });
            }
            return View(obj);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não foi fornecido!" });
            }
            var obj = await _empresaService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado!" });
            }
            EmpresaFormViewModel viewModel = new EmpresaFormViewModel { Empresa = obj };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Empresa empresa)
        {
            if (!ModelState.IsValid) // Teste de validação
            {
                var viewModel = new EmpresaFormViewModel { Empresa = empresa };
                return View(viewModel);
            }
            if (id != empresa.Id)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não corresponde com o da chamada!" });
            }
            try
            {
                await _empresaService.UpdateAsync(empresa);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Erro), new { message = e.Message });
            }
        }

        public IActionResult Erro(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
