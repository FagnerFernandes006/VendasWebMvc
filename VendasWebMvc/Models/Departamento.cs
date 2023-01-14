using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Departamento
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} não Preenchido")] //Campo requerido
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve ser entre {2} a {1} caracteres")] // Tamanho máximo da string e mínimo de caracteres 
        public string Nome { get; set; }
        public Empresa Empresa { get; set; }
        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();

        public Departamento()
        {
        }

        public Departamento(int id, string nome, Empresa empresa)
        {
            Id = id;
            Nome = nome;
            Empresa = empresa;
        }

        public void AddVendedor(Vendedor vendedor)
        {
            Vendedores.Add(vendedor);
        }
        public void RemoveVendedor(Vendedor vendedor)
        {
            Vendedores.Remove(vendedor);
        }
        public double TotalVendas(DateTime inicio, DateTime final)
        {
            return Vendedores.Sum(v => v.TotalVendas(inicio, final));
        }
    }
}
