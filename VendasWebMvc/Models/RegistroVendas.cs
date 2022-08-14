using System;
using VendasWebMvc.Models.Enums;

namespace VendasWebMvc.Models
{
    public class RegistroVendas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Qtde { get; set; }
        public StatusVenda StatusVenda { get; set; }
        public Vendedor Vendedor { get; set; }

        public RegistroVendas()
        {
        }

        public RegistroVendas(int id, DateTime data, double qtde, StatusVenda statusVenda, Vendedor vendedor)
        {
            Id = id;
            Data = data;
            Qtde = qtde;
            StatusVenda = statusVenda;
            Vendedor = vendedor;
        }
    }
}
