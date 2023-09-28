using System.ComponentModel.DataAnnotations;

namespace iti.ViewModelClasses
{
    public class RoleViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
