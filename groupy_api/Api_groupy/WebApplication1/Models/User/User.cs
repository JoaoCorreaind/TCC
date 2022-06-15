using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [StringLength(maximumLength:100)]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        [JsonIgnore]
        public virtual ICollection<Grupo> Grupos { get; set; }
        public string TokenUsuario { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

    }
    public class DadosLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
    }
}
