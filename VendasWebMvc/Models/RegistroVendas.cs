using System;
using System.ComponentModel.DataAnnotations;
using VendasWebMvc.Models.Enums;

namespace VendasWebMvc.Models
{
    public class RegistroVendas
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        [Display(Name = "Quantia")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Qtde { get; set; }
        [Display(Name = "Status")]
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
