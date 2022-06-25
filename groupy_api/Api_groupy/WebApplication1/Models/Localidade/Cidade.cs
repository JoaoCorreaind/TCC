using System.Collections.Generic;

namespace WebApplication1.Models.Localidade
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Estado Estado { get; set; }
        public List<Grupo> Grupos { get; set; }
        public List<User> Users { get; set; }

    }
}
