using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using WebApplication1.Models.Localidade;

namespace WebApplication1.Models
{
    public class Grupo
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha as informações diversas")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "O titulo do grupo deve ter de 4 a 30 caracteres")]
        public string Title { get; set; }
        public string Descricao { get; set; }
        public int MaximoUsuarios { get; set; }
        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Id do lider deve ser informado")]
        public int LiderId { get; set; }
        [NotMapped]
        public User Lider { get; set; }
        [MaxLength(200)]
        public string GrupoMainImage { get; set; }
        public List<ImageModel> GrupoImages { get; set; }
        public Cidade Cidade { get; set; }
        public List<User> Participantes { get; set; }
        public List<Tag> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }        
    }
}
