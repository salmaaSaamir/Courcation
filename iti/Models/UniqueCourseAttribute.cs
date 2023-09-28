using System.ComponentModel.DataAnnotations;

namespace iti.Models
{
    public class UniqueCourseAttribute : ValidationAttribute
    {
        public string ErrorMessage { get; set; } = "Course Code Must Be Unique";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string newCode = value.ToString();
            var course = (Course)validationContext.ObjectInstance;

            Data db = new Data();
            var existingCourse = db.Courses.FirstOrDefault(s => s.CourseCode == newCode);

            if (existingCourse != null && existingCourse.Id != course.Id)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

    }
}
