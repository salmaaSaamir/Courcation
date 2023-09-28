using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using iti.Models;
using System.Security.Cryptography;
using iti.ViewModelClasses;
using iti.Repos;
using Microsoft.Build.Framework;

namespace iti.Controllers
{
    public class StudentsCoursesController : Controller
    {
        IStudentCoursesRepo studentCoursesRepo;
        ICoursesRepo courseRepo;
        IStudentRepo studentRepo;
        IDepartmentRepo deptRepo;

        public StudentsCoursesController(IStudentCoursesRepo _studentCoursesRepo, ICoursesRepo _CourseRepo, IStudentRepo _stdRepo, IDepartmentRepo _deptRepo)
        {
            studentCoursesRepo = _studentCoursesRepo;
            courseRepo = _CourseRepo;
            studentRepo = _stdRepo;
            deptRepo = _deptRepo;
        }
        public IActionResult StudentCoursesDetailes(int id)
        {
            var student = studentCoursesRepo.GetSpecificStudent(id);

            ViewBag.StudentId = id;
            var stdCourses = studentCoursesRepo.GetSpecificStudentCourses(id);
            var TotalStuedntHour = 0;
            if (student != null)
            {
                ViewBag.StudentGpa = student.Gpa;
                ViewBag.StudentName = student.Name;
                ViewBag.StudentLevel = student.Level;
                ViewBag.StudentId = student.Id;
                ViewBag.Image = student.Image;
                if (stdCourses != null)
                {
                    foreach (var item in stdCourses)
                    {
                        TotalStuedntHour += item.Course.Hours;
                    }
                }
                else
                {
                    ViewBag.StudentTotalHours = TotalStuedntHour;
                }
                ViewBag.StudentTotalHours = TotalStuedntHour;
                return View(studentCoursesRepo.StudentCoursesDetailes(id));
            }
            return View(studentCoursesRepo.StudentCoursesDetailes(id));

        }
        public IActionResult EnrollCourse(int id)
        {
            ViewBag.StdId = studentRepo.GetSpecificStudent(id);
            ViewBag.Courses = studentCoursesRepo.CoursesAvailableToStudentToEnroll(id);
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEnroll(StudentsCourses obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Course_id != 0)
                {
                    studentCoursesRepo.SaveEnrolledCourse(obj);
                    return RedirectToAction("StudentCoursesDetailes", new { id = obj.Student_id });
                }
                else
                {
                    ModelState.AddModelError("", "you have select course to enroll it");
                    return View("EnrollCourse", obj);
                }
            }
            return View("EnrollCourse", obj);
        }
        public IActionResult Edite(int id, int cId)
        {
            return View(studentCoursesRepo.EditeEnrolledCourse(id,cId));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdite(StudentsCourses obj)
        {
            if (ModelState.IsValid)
            {
                studentCoursesRepo.SaveEditeStudentCourse(obj);
               return RedirectToAction("StudentCoursesDetailes", new { id = obj.Student_id });
            }
            return View("Edite", obj);
        }

       

    }
}
