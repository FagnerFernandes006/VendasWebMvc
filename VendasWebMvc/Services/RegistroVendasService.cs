using System;
using System.Collections.Generic;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VendasWebMvc.Services
{
    public class RegistroVendasService
    {
        private readonly VendasWebMvcContext _context;

        public RegistroVendasService(VendasWebMvcContext context)
        {
            _context = context;
        }
        public async Task<List<RegistroVendas>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.RegistroVendas select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }
            return await result.Include(x => x.Vendedor).Include(x => x.Vendedor.Departamento).OrderByDescending(x => x.Data).ToListAsync();
        }
        public async Task<List<IGrouping<Departamento,RegistroVendas>>> FindByDateGroupAsync(DateTime? minDate, DateTime? maxDate)
        {
            var result = from obj in _context.RegistroVendas select obj;
            if (minDate.HasValue)
            {
                result = result.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                result = result.Where(x => x.Data <= maxDate.Value);
            }
            return await result.Include(x => x.Vendedor).Include(x => x.Vendedor.Departamento).OrderByDescending(x => x.Data).GroupBy(x => x.Vendedor.Departamento).ToListAsync();
        }
        public async Task<List<RegistroVendas>> FindAllAsync()
        {
            return await _context.RegistroVendas.Include(x => x.Vendedor).ToListAsync();
        }
        public async Task InserirAsync(RegistroVendas obj)
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }
    }
}
