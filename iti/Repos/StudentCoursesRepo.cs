using iti.Models;
using iti.ViewModelClasses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace iti.Repos
{
    public class StudentCoursesRepo : IStudentCoursesRepo
    {
        IStudentRepo stdRepo;

        Data db;
        public StudentCoursesRepo(Data _db , IStudentRepo _stdRepo)
        {
            db = _db;
           stdRepo = _stdRepo;
        }
        public void CalculateGpa(StudentsCourses obj)
        {
            var stdCourse = EditeEnrolledCourse(obj.Student_id, obj.Course_id);
            stdCourse.Degree = obj.Degree;
            var std = stdRepo.GetSpecificStudent(obj.Student_id);
            var stdCourses = db.StudentCourses.Include("Course").Where(x => x.Student_id == obj.Student_id).ToList();
            var GradePoint = 0.0;
            var TotalGradePoints = 0.0;
            var TotalStdHours = db.StudentCourses.Include("Course").Where(x => x.Student_id == obj.Student_id).Sum(x => x.Course.Hours);
            foreach (var item in stdCourses)
            {
                if (item.Degree >= 50 && item.Degree < 55)
                {
                    GradePoint = 1.3 * item.Course.Hours;
                }
                else if (item.Degree >= 55 && item.Degree < 60)
                {
                    GradePoint = 1.7 * item.Course.Hours;
                }
                else if (item.Degree >= 60 && item.Degree < 65)
                {
                    GradePoint = 2.0 * item.Course.Hours;
                }
                else if (item.Degree >= 65 && item.Degree < 70)
                {
                    GradePoint = 2.3 * item.Course.Hours;
                }
                else if (item.Degree >= 70 && item.Degree < 75)
                {
                    GradePoint = 2.7 * item.Course.Hours;
                }
                else if (item.Degree >= 75 && item.Degree < 80)
                {
                    GradePoint = 3.0;
                }
                else if (item.Degree >= 80 && item.Degree < 85)
                {
                    GradePoint = 3.3 * item.Course.Hours;
                }
                else if (item.Degree >= 85 && item.Degree < 90)
                {
                    GradePoint = 3.7 * item.Course.Hours;
                }
                else if (item.Degree >= 90 && item.Degree <= 100)
                {
                    GradePoint = 4.0 * item.Course.Hours;
                }

                TotalGradePoints += GradePoint;
            }
            std.Gpa = (float)Math.Round(TotalGradePoints / TotalStdHours, 2);
        }
        public List<StuedntCourse> StudentCoursesDetailes(int id)
        {
            var courses = db.StudentCourses.Include("Course").Include("Student").Where(x => x.Student_id == id).ToList();
            var student = db.Students.FirstOrDefault(x => x.Id == id);
            List<StuedntCourse> studentsWithCourses = new List<StuedntCourse>();
            foreach (var item in courses)
            {
                StuedntCourse studentWithCourse = new StuedntCourse()
                {

                    CourseCode = item.Course.CourseCode,
                    CourseName = item.Course.CourseName,
                    Hours = item.Course.Hours,
                    Degree = item.Degree,
                    CourseId = item.Course_id,
                };
                if (item.Degree == 0)
                {
                    studentWithCourse.CourseStatus = "learning";
                }
                else if (item.Degree < 50 && item.Degree !=0)
                {
                    studentWithCourse.Color = "#fdbdc4";
                    studentWithCourse.CourseStatus = "failed";

                }
                else
                {
                    studentWithCourse.CourseStatus = "passed";
                }

                studentsWithCourses.Add(studentWithCourse);
            }
            return studentsWithCourses;
        }
        public Student GetSpecificStudent(int id)
        {
            return db.Students.FirstOrDefault(x => x.Id == id);
        }
        public List<StudentsCourses> GetSpecificStudentCourses(int id)
        {
            return db.StudentCourses.Include("Course").Where(x => x.Student_id == id).ToList();
            
        }
        public List<int> EnrollCourse(int id)
        {
            return db.StudentCourses.Where(x => x.Student_id == id).Select(x => x.Course_id).ToList();
        }
        public List<Course> CoursesAvailableToStudentToEnroll(int id)
        {
            var student = stdRepo.GetSpecificStudent(id);
            var enrolledCourses = EnrollCourse(id);
            return db.Courses.Where(x => x.Dept_Id == student.Dept && !enrolledCourses.Contains(x.Id)).ToList();
        }
        public  void SaveEnrolledCourse(StudentsCourses obj)
        {
            db.StudentCourses.Add(obj);
            db.SaveChanges();
        }

        public StudentsCourses EditeEnrolledCourse(int id , int cId)
        {
            var studentCourse = db.StudentCourses.Include("Student").Include("Course").FirstOrDefault(x => x.Student_id == id && x.Course_id == cId);
            return studentCourse;
        }
        public void SaveEditeStudentCourse(StudentsCourses obj)
        {
            if (obj.Course_id != 0 || obj.Student_id != 0)
            {
                CalculateGpa(obj);
                db.SaveChanges();
            }
        }


    }
}
