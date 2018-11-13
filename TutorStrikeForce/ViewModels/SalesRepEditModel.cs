
using System.ComponentModel.DataAnnotations;


namespace TutorStrikeForce.ViewModels
{
    public class SalesRepEditModel
    {
        public int? SalesRepId { get; set; }
        [Required]
        [MaxLength(45)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(45)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
    }
}
