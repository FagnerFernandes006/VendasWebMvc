using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
     
        [Required(ErrorMessage = "{0} não Preenchido")] //Campo requerido
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve ser entre {2} a {1} caracteres")] // Tamanho máximo da string e mínimo de caracteres 
        public string Nome { get; set; }
      
        [Required(ErrorMessage = "{0} não Preenchido")]
        [DataType(DataType.EmailAddress)] //torna um link de email 
        [EmailAddress(ErrorMessage = "Insira um email válido")] //Formato de email valido
        public string Email { get; set; }
      
        [Display(Name = "Data de Nascimento")] //Exibir na view ao inves do "DataNascimento"
        [DataType(DataType.Date)] //Somente a data sem o horário
        [Required(ErrorMessage = "{0} não Preenchido")]
        public DateTime DataNascimento { get; set; }
    
        [Display(Name = "Salário Base")] //Exibir na view ao inves do "SalarioBase"
        [DisplayFormat(DataFormatString = "{0:F2}")] //Exibir com duas casas decimais
        [Required(ErrorMessage = "{0} não Preenchido")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} deve ser entre {1} a {2}")] // mínimo e máxima do salario
        public double SalarioBase { get; set; }
        public Departamento Departamento { get; set; }
        public Empresa Empresa { get; set; }
        [Display(Name = "Departamento")]
        public int DepartamentoId { get; set; }

        [Display(Name = "Empresa")]
        public int EmpresaId { get; set; }
        public ICollection<RegistroVendas> Vendas { get; set; } = new List<RegistroVendas>();

        public Vendedor()
        {
        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento, Empresa empresa)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
            Empresa = empresa;
        }

        public void AddVendas(RegistroVendas rv)
        {
            Vendas.Add(rv);
        }
        public void RemoveVendas(RegistroVendas rv)
        {
            Vendas.Remove(rv);
        }

        public double TotalVendas(DateTime inicio, DateTime final)
        {
            return Vendas.Where(rv => rv.Data >= inicio && rv.Data <= final).Sum(rv => rv.Qtde);
        }
    }
}
