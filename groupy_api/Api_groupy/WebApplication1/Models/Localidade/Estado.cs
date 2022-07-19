using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace WebApplication1.Models.Localidade
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Uf { get; set; }
        [JsonIgnore]
        public virtual ICollection<Cidade> Cidades { get; set; }

    }
}
