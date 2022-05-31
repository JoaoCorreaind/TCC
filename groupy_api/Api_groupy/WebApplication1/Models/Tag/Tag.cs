using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public bool IsDeleted { get; set; }
        [JsonIgnore]
        public List<Grupo> Grupos { get; set; }      

    }
}
