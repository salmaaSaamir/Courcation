using System.ComponentModel.DataAnnotations;

namespace iti.Models
{
    public class UniqueDepartmentAttribute : ValidationAttribute
    {
        public string ErrorMessage { get; set; } = "Department Code Must Be Unique";

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            string newCode = value.ToString();
            var dept = (Department)validationContext.ObjectInstance;

            Data db = new Data();
            var existingDept = db.Departments.FirstOrDefault(s => s.Code == newCode);

            if (existingDept != null && existingDept.Id != dept.Id)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }
    
    }
}
