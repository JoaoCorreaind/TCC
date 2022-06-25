using System.Collections.Generic;

namespace WebApplication1.Models.Localidade
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        public virtual ICollection<Cidade> Cidades { get; set; }

    }
}
