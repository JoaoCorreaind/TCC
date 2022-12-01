using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Preencha as informações diversas")]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "O titulo do grupo deve ter de 4 a 30 caracteres")]
        public List<ChatMessage> Messages { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserLimit { get; set; }
        public bool IsPresencial { get; set; }

        public bool IsDeleted { get; set; }
        [Required(ErrorMessage = "Id do lider deve ser informado")]
        public string LeaderId { get; set; }
        [NotMapped]
        public User Leader { get; set; }
        [MaxLength(200)]
        public ImageModel GroupMainImage { get; set; }
        public List<ImageModel> GroupImages { get; set; }
        public int? AddressId { get; set; }
        public Address? Address { get; set; }
        public List<User> Participants { get; set; }
        public List<Tag> Tags { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DeletedAt { get; set; }        
    }
}
