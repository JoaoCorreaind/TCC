using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class ApprovalRequest
    {
        [Required]
        public string UserToApproveId { get; set; } //id do usuario que pediu aprovação
        [Required]
        public string DestinationGroupyId { get; set; }  //id do grupo para qual o usuario pediu aprovação
        public User UserToApprove { get; set; }  //grupo para o qual o usuario pediu aprovação
        public Group DestinationGroupy { get; set; } //grupo onde para qual a request foi enviada
        
        public string Message { get; set; } //mensagem 
        public bool Approvated { get; set; } //aprovado ou não
        public DateTime AppovatedAt { get; set; } //data de aprovação
        public DateTime CreatedAt { get; set; } //data de registro no banco
        public DateTime UpdatedAt { get; set; } //data de update do registro no banco
    }
}
