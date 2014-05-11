using System.ComponentModel.DataAnnotations;

namespace RobIII.Models
{
    public class ContactformViewmodel
    {
        [Required]
        [StringLength(150)]
        [Display(Order = 1)]
        public string Name { get; set; }

        [Required]
        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Display(Order = 2)]
        public string Email { get; set; }

        [Required]
        [Display(Order = 3)]

        public string Message { get; set; }

        public string Schd { get; set; }
    }
}