using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iti.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="course name is required")]
        [MinLength(2 ,ErrorMessage = "course name must be more than or  2 characters")]
        public string CourseName { get; set; }
        [Required(ErrorMessage = "course code is required")]
        [UniqueCourse]
        public string CourseCode { get; set; }
        [Required(ErrorMessage = "hours of course are required")]
        public int Hours { get; set;}
        [ForeignKey("Department")]
        public int Dept_Id { get; set; }
        public  Department? Department { get; set; }
    }
}
