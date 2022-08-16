using System.Collections.Generic;
using System.Linq;
using VendasWebMvc.Data;
using VendasWebMvc.Models;
using System;
using Microsoft.EntityFrameworkCore;
using VendasWebMvc.Services.Exceptions;

namespace VendasWebMvc.Services
{
    public class VendedorService
    {
        private readonly VendasWebMvcContext _context;

        public VendedorService(VendasWebMvcContext context)
        {
            _context = context;
        }

        public List<Vendedor> FindAll()
        {
            return _context.Vendedor.ToList();
        }

        public void Inserir(Vendedor obj)
        {
            _context.Add(obj);
            _context.SaveChanges();
        }
        public Vendedor FindById(int id)
        {
            return _context.Vendedor.Include(obj => obj.Departamento).FirstOrDefault(obj => obj.Id == id);
        }
        public void Remover(int id)
        {
            var obj = _context.Vendedor.Find(id);
            _context.Vendedor.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(Vendedor obj)
        {
            if (!_context.Vendedor.Any(x => x.Id == obj.Id))
            {
                throw new NotFoundException("Id não encontrado!");
            }
            try
            {
                _context.Update(obj);
                _context.SaveChanges();
            }
            catch(DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}
