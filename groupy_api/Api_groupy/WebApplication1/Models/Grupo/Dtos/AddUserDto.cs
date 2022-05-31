using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class AddUserDto
    {
        [Required]
        public int GrupoId { get; set; }
        public int UserId { get; set; }
    }
}
