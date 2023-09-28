using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace iti.Models
{
    public class StudentsCourses
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Student")]
        public int Student_id { get; set; }
        public Student? Student { get; set; }
        [Required(ErrorMessage = "choosing course  is required")]
        [ForeignKey("Course")]
        public int Course_id { get; set; }
        public Course? Course { get; set; }
        [Required(ErrorMessage = "the degree  is required")]
        [Range(0, 100,ErrorMessage ="the degree must be between 0 and 100")]
        public int Degree { get; set; }

    }
}
