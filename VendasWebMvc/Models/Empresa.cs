using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VendasWebMvc.Models
{
    public class Empresa
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} não Preenchido")] //Campo requerido
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} deve ser entre {2} a {1} caracteres")] // Tamanho máximo da string e mínimo de caracteres 
        public string Nome { get; set; }
        [Required(ErrorMessage = "{0} não Preenchido")] //Campo requerido
        [DisplayFormat(DataFormatString = @"{0:00\.000\.000\/0000-00}")]
        [MinLength(14, ErrorMessage = "{0} deve ser 14 numeros"), MaxLength(14, ErrorMessage = "{0} deve ser 14 numeros")]
        public long CNPJ { get; set; }
        [Required(ErrorMessage = "{0} não Preenchido")] //Campo requerido
        [Display(Name = "Nome Fantasia")]
        public string NomeFantasia { get; set; }
        public ICollection<Departamento> Departamentos { get; set; } = new List<Departamento>();
        public ICollection<Vendedor> Vendedores { get; set; } = new List<Vendedor>();


        public Empresa()
        {
        }

        public Empresa(int id, string nome, long cNPJ, string nomeFantasia)
        {
            Id = id;
            Nome = nome;
            CNPJ = cNPJ;
            NomeFantasia = nomeFantasia;
        }
    }
}
