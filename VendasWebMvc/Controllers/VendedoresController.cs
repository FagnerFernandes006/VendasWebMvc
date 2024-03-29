﻿using Microsoft.AspNetCore.Mvc;
using VendasWebMvc.Models;
using VendasWebMvc.Services;
using VendasWebMvc.Models.ViewModels;
using System.Collections.Generic;
using VendasWebMvc.Services.Exceptions;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

namespace VendasWebMvc.Controllers
{
    public class VendedoresController : Controller
    {
        private readonly VendedorService _vendedorService;
        private readonly DepartamentoService _departamentoService;
        private readonly EmpresaService _empresaService;

        public VendedoresController(VendedorService vendedorService, DepartamentoService departamentoService, EmpresaService empresaService)
        {
            _vendedorService = vendedorService;
            _empresaService = empresaService;
            _departamentoService = departamentoService;
        }
        public async Task<IActionResult> Index()
        {
            var list = await _vendedorService.FindAllAsync();
            return View(list);
        }
        public async Task<IActionResult> Criar()
        {
            var departamentos = await _departamentoService.FindAllAsync();
            var empresas = await _empresaService.FindAllAsync();
            var viewModel = new VendedorFormViewModel { Departamentos = departamentos, Empresas = empresas };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Vendedor vendedor)
        {
            if (!ModelState.IsValid) // Teste de validação
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var empresas = await _empresaService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos, Empresas = empresas };
                return View(viewModel);
            }
            await _vendedorService.InserirAsync(vendedor);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Deletar(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não foi fornecido!" });
            }
            var obj = await _vendedorService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado!" });
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deletar(Vendedor vendedor)
        {
            try
            {
                await _vendedorService.RemoverAsync(vendedor.Id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(Erro), new { message = "Não é possível deletar um vendedor que possui vendas!" });
            }
        }

        public async Task<IActionResult> Detalhes(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não foi fornecido!" });
            }
            var obj = await _vendedorService.FindByIdAsync(id.Value);
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
            var obj = await _vendedorService.FindByIdAsync(id.Value);

            if (obj == null)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não encontrado!" });
            }

            List<Departamento> departamentos = await _departamentoService.FindAllAsync();
            List<Empresa> empresas = await _empresaService.FindAllAsync();
            VendedorFormViewModel viewModel = new VendedorFormViewModel { Vendedor = obj, Departamentos = departamentos, Empresas = empresas };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int id, Vendedor vendedor)
        {
            if (!ModelState.IsValid) // Teste de validação
            {
                var departamentos = await _departamentoService.FindAllAsync();
                var empresas = await _empresaService.FindAllAsync();
                var viewModel = new VendedorFormViewModel { Vendedor = vendedor, Departamentos = departamentos, Empresas = empresas };
                return View(viewModel);
            }
            if (id != vendedor.Id)
            {
                return RedirectToAction(nameof(Erro), new { message = "Id não corresponde com o da chamada!" });
            }
            try
            {
                await _vendedorService.UpdateAsync(vendedor);
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
