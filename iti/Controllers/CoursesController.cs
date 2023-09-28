using Microsoft.AspNetCore.Mvc;
using iti.Repos;
using iti.Models;

namespace iti.Controllers
{
    public class CoursesController : Controller
    {
        ICoursesRepo courseRepo;
        IStudentRepo studentRepo;
        IDepartmentRepo deptRepo;
        IStudentCoursesRepo studentCoursesRepo;
        public CoursesController(ICoursesRepo _CourseRepo,IStudentRepo _stdRepo, IDepartmentRepo _deptRepo ,IStudentCoursesRepo _studentCoursesRepo)
        {
            courseRepo = _CourseRepo;   
            studentRepo = _stdRepo;
            deptRepo = _deptRepo;
            studentCoursesRepo = _studentCoursesRepo;
        }
        public IActionResult Index()
        {
            return View(courseRepo.GetAll());
        }

        public IActionResult GetSpecificCourses(int id)
        {
            ViewBag.DeptName = deptRepo.GetSpecificDept(id)?.Name;
            ViewBag.totalCourses = courseRepo.GetSpecificCourses(id).Count();
            return View(courseRepo.GetSpecificCourses(id));
        }
        public IActionResult Create()
        {
            ViewData["Department"] = deptRepo.GetAll();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveCourse(Course obj)
        {
            if (ModelState.IsValid)
            {
               courseRepo.SaveCourse(obj);
                return RedirectToAction("Index");
            }
            ViewData["Department"] = deptRepo.GetAll();
            return View("Create", obj);
        }
        public IActionResult Edite(int? id)
        {
            ViewData["Department"] =deptRepo.GetAll();
            return View(courseRepo.Edite(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdite(Course course)
        {
            if (ModelState.IsValid)
            {
                courseRepo.SaveEdite(course);
                return RedirectToAction("Index");
            }
            ViewData["Department"] = deptRepo.GetAll();
            return View("Edite", course);
        }
        public IActionResult Delete(int id)
        {
            var course = courseRepo.GetSpecificCourse(id);
            return View(course);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                var course = courseRepo.GetSpecificCourse(id);
                var student = studentRepo.GetSpecificStudent(id);
                courseRepo.DeleteConfirmed(course);
                return RedirectToAction("Index");
            }
            return View("Delete",id);
        }
    }
}
