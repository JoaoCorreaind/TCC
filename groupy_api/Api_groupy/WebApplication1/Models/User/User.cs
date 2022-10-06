using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class User : IdentityUser
    {
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string Image { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string About { get; set; }
        public List<Group> Groups { get; set; }
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
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string About { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public string CityId { get; set; }
        public string ZipCode { get; set; }
        public string District { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ReferencePoint { get; set; }
        public string Image { get; set; }

    }
}
