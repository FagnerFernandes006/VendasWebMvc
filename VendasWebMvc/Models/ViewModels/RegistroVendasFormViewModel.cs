using System.Collections.Generic;

namespace VendasWebMvc.Models.ViewModels
{
    public class RegistroVendasFormViewModel
    {
        public RegistroVendas RegistroVendas { get; set; }
        public ICollection<Vendedor> Vendedores { get; set; }
    }
}
