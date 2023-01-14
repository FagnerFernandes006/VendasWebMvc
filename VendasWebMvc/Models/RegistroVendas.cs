using System;
using System.ComponentModel.DataAnnotations;
using VendasWebMvc.Models.Enums;

namespace VendasWebMvc.Models
{
    public class RegistroVendas
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} não Preenchido")]
        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [Required(ErrorMessage = "{0} não Preenchido")]
        [Display(Name = "Valor")]
        [DisplayFormat(DataFormatString = "{0:F2}")]
        [Range(100.0, 900000.0, ErrorMessage = "{0} deve ser entre {1} a {2}")]
        public double Qtde { get; set; }
        [Display(Name = "Status")]
        [Required(ErrorMessage = "{0} não Preenchido")]
        public StatusVenda StatusVenda { get; set; }
        public Vendedor Vendedor { get; set; }
        [Required(ErrorMessage = "{0} não Preenchido")]

        [Display(Name = "Vendedor")]
        public int VendedorId { get; set; }

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
