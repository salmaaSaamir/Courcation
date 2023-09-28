using System.ComponentModel.DataAnnotations;

namespace iti.ViewModelClasses
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool remmeberMe { get; set; }
    }
}
