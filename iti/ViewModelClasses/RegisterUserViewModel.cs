using iti.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace iti.ViewModelClasses
{
    public class RegisterUserViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "the Name must be less then 25 letters")]
        [MinLength(3, ErrorMessage = "the Name must be more then 2 letters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "user code is required")]
        public int Code { get; set; }
        [Required(ErrorMessage ="the password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "the password is required to confirm")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
