using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iti.Models
{
    public class Student
    {

        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(25, ErrorMessage = "the Name must be less then 25 letters")]
        [MinLength(3,ErrorMessage ="the Name must be more then 2 letters")]
        public string Name { get; set; }

        [Required(ErrorMessage ="student code is required")]
        [UniqueStudent(ErrorMessage = "Student Code Must Be Unique")]
        public string Code { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [Range(18,24 , ErrorMessage = "the age of the student must be between 18 and 24")]
        public int Age { get; set; }
        [Required]
        [Range(1, 4, ErrorMessage = "the level of the student must be between 1 and 4")]
        public string Level { get; set; }
        [Required]
        [Range(0, 4, ErrorMessage = "the gpa of the student must be between 0 and 4")]
        public float Gpa { get; set; }
        [Required(ErrorMessage = "student img is required")]

        [RegularExpression(@"\w+\.(jpg|png)", ErrorMessage = "the image must be of type jpg or png")]
        public string Image { get; set; }
        [NotMapped]
        
        public IFormFile? ImageFile { get; set; }
        [ForeignKey("Department")]
        public int Dept { get; set; }
        public Department? Department { get; set; }
    }
}
