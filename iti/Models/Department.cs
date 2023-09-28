
using System.ComponentModel.DataAnnotations;

namespace iti.Models
{
    public class Department
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="the department name is required")]
        [MinLength(2,ErrorMessage ="the department name must be more than 1 character")]
        [MaxLength(5 ,ErrorMessage ="the department name must be less ten 6 letters")]
        public string Name { get; set; }
        [Required(ErrorMessage ="the department code is required")]
        [UniqueDepartment]
        public string Code { get; set; }
        [Required(ErrorMessage = "the department manager's name is required")]
        [MinLength(2, ErrorMessage = "the department manager's name must be more than 2 letters")]
        [MaxLength(25, ErrorMessage = "the department manager's name must be less than 25 letters")]

        public string ManagerName { get; set; }
    }
}