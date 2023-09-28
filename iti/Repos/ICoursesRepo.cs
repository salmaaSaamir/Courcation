using iti.Models;

namespace iti.Repos
{
    public interface ICoursesRepo
    {
        List<Course> GetAll();
        List<Course> GetSpecificCourses(int id);
        Course GetSpecificCourse(int id);
        void SaveCourse(Course obj);
        Course Edite(int? id);
        void SaveEdite(Course course);
        void DeleteConfirmed(Course obj);
    }
}