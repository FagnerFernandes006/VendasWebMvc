using System.Linq;
using VendasWebMvc.Models;
using System;
using VendasWebMvc.Models.Enums;

namespace VendasWebMvc.Data
{
    public class SeedingService
    {
        private VendasWebMvcContext _context;

        public SeedingService(VendasWebMvcContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Departamento.Any() || _context.Vendedor.Any() || _context.RegistroVendas.Any())
            {
                return; // o banco de dados ja foi populado
            }
            Empresa e1 = new Empresa(1, "FagnerLTDA", 12345678904567, "Programador");

            Departamento d1 = new Departamento(1, "Eletrônicos", e1);
            Departamento d2 = new Departamento(2, "Computadores", e1);
            Departamento d3 = new Departamento(3, "Peças", e1);
            Departamento d4 = new Departamento(4, "Usados", e1);

            Vendedor v1 = new Vendedor(1, "Fagner Fernandes", "Fagner.b.fernandes@gmail.com", new DateTime(2001, 03, 03), 1500.00, d1, e1);
            Vendedor v2 = new Vendedor(2, "Matheus White", "Matheus.Colucci@gmail.com", new DateTime(1998, 07, 11), 1300.00, d2, e1);

            RegistroVendas r1 = new RegistroVendas(1, new DateTime(2022, 02, 15), 11000.0, StatusVenda.Faturado, v1);
            RegistroVendas r2 = new RegistroVendas(2, new DateTime(2022, 05, 03), 5000.0, StatusVenda.Cancelado, v1);
            RegistroVendas r3 = new RegistroVendas(3, new DateTime(2022, 04, 08), 6500.0, StatusVenda.Faturado, v2);
            RegistroVendas r4 = new RegistroVendas(4, new DateTime(2022, 08, 10), 7000.0, StatusVenda.Pedente, v2);

            _context.Empresa.Add(e1);
            _context.Departamento.AddRange(d1, d2, d3, d4);
            _context.Vendedor.AddRange(v1, v2);
            _context.RegistroVendas.AddRange(r1, r2, r3, r4);

            _context.SaveChanges();
                }
    }
}
