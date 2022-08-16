using VendasWebMvc.Data;
using System.Collections.Generic;
using System.Linq;
using VendasWebMvc.Models;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Departamento.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
