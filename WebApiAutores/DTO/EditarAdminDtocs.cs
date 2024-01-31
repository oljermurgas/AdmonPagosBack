using System.ComponentModel.DataAnnotations;

namespace AdminPagosApi.DTO
{
    public class EditarAdminDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }   
    }
}
