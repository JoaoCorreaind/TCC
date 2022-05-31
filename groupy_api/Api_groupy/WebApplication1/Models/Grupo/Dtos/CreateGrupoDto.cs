using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CreateGrupoDto
    {
        public string Descricao { get; set; } = "Não Informado";
        public int MaximoUsuarios { get; set; } = 10;
        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public int LiderId { get; set; }
        public List<int> Tags { get; set; }
    }
}
