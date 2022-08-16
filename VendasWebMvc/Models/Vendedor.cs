using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VendasWebMvc.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        [DataType(DataType.EmailAddress)] //torna um link de email 
        public string Email { get; set; }

        [Display(Name = "Data de Nascimento")] //Exibir na view ao inves do "DataNascimento"
        [DataType(DataType.Date)] //Somente a data sem o horário
        public DateTime DataNascimento { get; set; }

        [Display(Name = "Salário Base")] //Exibir na view ao inves do "SalarioBase"
        [DisplayFormat(DataFormatString = "{0:F2}")] //Exibir com duas casas decimais
        public double SalarioBase { get; set; }
        public Departamento Departamento { get; set; }
        public int DepartamentoId { get; set; }
        public ICollection<RegistroVendas> Vendas { get; set; } = new List<RegistroVendas>();

        public Vendedor()
        {
        }

        public Vendedor(int id, string nome, string email, DateTime dataNascimento, double salarioBase, Departamento departamento)
        {
            Id = id;
            Nome = nome;
            Email = email;
            DataNascimento = dataNascimento;
            SalarioBase = salarioBase;
            Departamento = departamento;
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
