using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using VendasWebMvc.Services.Exceptions;

namespace VendasWebMvc.Services
{
    public class EmpresaService
    {
        private readonly VendasWebMvcContext _context;

        public EmpresaService(VendasWebMvcContext context)
        {
            _context = context;
        }

        public async Task<List<Empresa>> FindAllAsync()
        {
            return await _context.Empresa.ToListAsync();
        }

        public async Task InserirAsync(Empresa obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
        public async Task<Empresa> FindByIdAsync(int id)
        {
            return await _context.Empresa.FirstOrDefaultAsync(obj => obj.Id == id);
        }
        public async Task<Vendedor> FindByIdAsyncVendedor(int id)
        {
            return await _context.Vendedor.FirstOrDefaultAsync(obj => obj.EmpresaId == id);
        }
        public async Task RemoverAsync(int id)
        {
            try
            {
                var obj = await _context.Empresa.FindAsync(id);
                _context.Empresa.Remove(obj);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new IntegrityException(ex.Message);
            }
        }
        public async Task UpdateAsync(Empresa obj)
        {
            if (!await _context.Vendedor.AnyAsync(x => x.Id == obj.Id))
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
