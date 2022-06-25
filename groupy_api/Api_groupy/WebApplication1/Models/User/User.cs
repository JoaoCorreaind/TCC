using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Localidade;

namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "campo email deve ser preenchido")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "O email deve possuir entre 15 e 100 caracteres")]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required(ErrorMessage = "Campo nome deve ser preenchido")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "O email deve possuir entre 4 e 100 caracteres")]
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Image { get; set; }
        public Cidade Cidade { get; set; }


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
        [Required(ErrorMessage = "Campo email deve ser preenchido")]
        [StringLength(100, MinimumLength = 15, ErrorMessage = "O email deve possuir entre 15 e 100 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo Senha deve ser preenchido")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "O senha deve possuir entre 6 e 100 caracteres")]
        public string Password { get; set; }

    }

    public class UserDto
    {
        [Required(ErrorMessage = "Campo Email deve ser preenchido")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "O email deve possuir entre 15 e 100 caracteres")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo senha deve ser preenchido")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "O senha deve possuir entre 6 e 100 caracteres")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Campo nome deve ser preenchido")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "O email deve possuir entre 6 e 100 caracteres")]
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public int CidadeId { get; set; }

        public IFormFile Image { get; set; } = null;

    }
}
