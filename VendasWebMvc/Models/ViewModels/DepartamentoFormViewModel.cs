using System.Collections.Generic;

namespace VendasWebMvc.Models.ViewModels
{
    public class DepartamentoFormViewModel
    {
        public Departamento Departamento { get; set; }
        public ICollection<Empresa> Empresas { get; set; }
    }
}
