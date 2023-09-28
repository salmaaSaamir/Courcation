using iti.Models;
using iti.ViewModelClasses;

namespace iti.Repos
{
    public interface IStudentCoursesRepo
    {
        List<StuedntCourse> StudentCoursesDetailes(int id);
        Student GetSpecificStudent(int id);
        List<StudentsCourses> GetSpecificStudentCourses(int id);
        List<int> EnrollCourse(int id);
        List<Course> CoursesAvailableToStudentToEnroll(int id);
        void SaveEnrolledCourse(StudentsCourses obj);
        StudentsCourses EditeEnrolledCourse(int id, int cId);
        void SaveEditeStudentCourse(StudentsCourses obj);
        void CalculateGpa(StudentsCourses obj);


        


    }
}