using iti.Models;
using iti.ViewModelClasses;
namespace iti.Repos
{
    public interface IStudentRepo
    {
        List<StudentWithDepartmentcs> GetAllStudents();
        void SaveInsertNewStudent(Student obj);
        Student GetSpecificStudent(int id);
        void ConfirmDeleteStudent(int id);
        Student Edite(int id);
        void SaveEditedStudent(Student obj);
    }
}