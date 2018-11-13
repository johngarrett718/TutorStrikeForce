using System.ComponentModel.DataAnnotations;

namespace TutorStrikeForce.ViewModels
{
    public class ClientEditModel
    {
        public int? ClientId { get; set; }
        [Required]
        [Display(Name = "Client Email")]
        [MaxLength(45)]
        public string Email { get; set; }
        [Required]
        [MaxLength(45)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(45)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [MaxLength(45)]
        public string City { get; set; }
        [Required]
        [Range(0.00, 100.00)]
        public decimal Hours { get; set; }
    }
}
