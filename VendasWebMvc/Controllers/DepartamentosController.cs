using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using VendasWebMvc.Models.ViewModels;
using VendasWebMvc.Services;

namespace VendasWebMvc.Controllers
{
    public class DepartamentosController : Controller
    {
        private readonly DepartamentoService _departamentoService;
        private readonly EmpresaService _empresaService;

        public DepartamentosController(DepartamentoService departamentoService, EmpresaService empresaService)
        {
            _empresaService = empresaService;
            _departamentoService = departamentoService;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var list = await _departamentoService.FindAllAsync();
                return View(list);
            }
            catch (Exception e)
            {

                throw e;
            }  
        }
        public async Task<IActionResult> Criar()
        {
            var empresas = await _empresaService.FindAllAsync();
            var viewModel = new DepartamentoFormViewModel { Empresas = empresas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Departamento departamento)
        {
            if (!ModelState.IsValid) // Teste de validação
            {
                var empresas = await _empresaService.FindAllAsync();
                var viewModel = new DepartamentoFormViewModel { Departamento = departamento, Empresas = empresas };
                return View(viewModel);
            }
            await _departamentoService.InserirAsync(departamento);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não foi fornecido!" });
            }
            var obj = await _departamentoService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado!" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(Departamento departamento)
        {
            try
            {
                await _departamentoService.RemoverAsync(departamento.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Erro), new { message = "Não é possível deletar!" });
            }
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não foi fornecido!" });
            }
            var obj = await _departamentoService.FindByIdAsync(id.Value);
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
            var obj = await _departamentoService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado!" });
            }

            List<Empresa> empresas = await _empresaService.FindAllAsync();
            DepartamentoFormViewModel viewModel = new DepartamentoFormViewModel { Departamento = obj, Empresas = empresas };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Departamento departamento)
        {
            if (!ModelState.IsValid) // Teste de validação
            {
                var empresas = await _empresaService.FindAllAsync();
                var viewModel = new DepartamentoFormViewModel { Departamento = departamento, Empresas = empresas };
                return View(viewModel);
            }
            if (id != departamento.Id)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não corresponde com o da chamada!" });
            }
            try
            {
                await _departamentoService.UpdateAsync(departamento);
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
