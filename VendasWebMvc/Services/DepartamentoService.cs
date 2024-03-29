﻿using VendasWebMvc.Data;
using System.Collections.Generic;
using System.Linq;
using VendasWebMvc.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Services.Exceptions;

namespace VendasWebMvc.Services
{
    public class DepartamentoService
    {
        private readonly VendasWebMvcContext _context;

        public DepartamentoService(VendasWebMvcContext context)
        {
            _context = context;
        }
        public async Task<List<Departamento>> FindAllAsync()
        {
            return await _context.Departamento.Include(x => x.Empresa).ToListAsync();
        }
        public async Task InserirAsync(Departamento obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Departamento> FindByIdAsync(int id)
        {
            return await _context.Departamento.Include(obj => obj.Empresa).FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task RemoverAsync(int id)
        {
            try
            {
                var obj = await _context.Departamento.FindAsync(id);
                _context.Departamento.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        public async Task UpdateAsync(Departamento obj)
        {
            if (!await _context.Departamento.AnyAsync(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
