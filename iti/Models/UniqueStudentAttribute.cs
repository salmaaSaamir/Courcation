using System.ComponentModel.DataAnnotations;


namespace iti.Models
{
    public class UniqueStudent : ValidationAttribute
    {
        public string ErrorMessage { get; set; } = "Student Code Must Be Unique";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string newCode = value.ToString();
            var student = (Student)validationContext.ObjectInstance;

            Data db = new Data();
            var existingStudent = db.Students.FirstOrDefault(s => s.Code == newCode);

            if (existingStudent != null && existingStudent.Id != student.Id)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    }
}
