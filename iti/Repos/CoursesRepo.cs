using iti.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace iti.Repos
{
    public class CoursesRepo: ICoursesRepo
    {
        readonly Data db;
        IStudentCoursesRepo studentCoursesRepo;
        public CoursesRepo(Data _db , IStudentCoursesRepo studentCoursesRepo)
        {
            db = _db;
            this.studentCoursesRepo = studentCoursesRepo;
        }
        public List<Course> GetAll()
        {
            var CoursesData = db.Courses.Include(c => c.Department).OrderBy(x => x.CourseCode).ToList();
            return CoursesData;
        }
        public List<Course> GetSpecificCourses(int id)
        {
            var data = db.Courses.Include(c => c.Department).Where(c => c.Dept_Id == id).ToList();
            return data;
        }
        public Course GetSpecificCourse(int id)
        {
            return db.Courses.Include("Department").FirstOrDefault(x => x.Id == id);
        }
        public void SaveCourse(Course obj)
        {
            db.Courses.Add(obj);
            db.SaveChanges();
        }
        public Course Edite(int? id)
        {
            var course = db.Courses.FirstOrDefault(x => x.Id == id);
            return course;
        }
        public void SaveEdite(Course course)
        {
            db.Courses.Update(course);
            db.SaveChanges();
        }
        public void DeleteConfirmed(Course obj)
        {
            db.Courses.Remove(obj);
            db.SaveChanges();
        }

    }
}
