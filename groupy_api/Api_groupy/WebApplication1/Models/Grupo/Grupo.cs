using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public int MaximoUsuarios { get; set; }
        public bool IsDeleted { get; set; }
        public int LiderId { get; set; }
        [NotMapped]
        public User Lider { get; set; }
        public List<User> Participantes { get; set; }
        public List<Tag> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }
    }
}
