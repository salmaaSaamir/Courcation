using iti.Models;
using Microsoft.AspNetCore.Mvc;
using iti.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace iti.Controllers
{

    public class StudentController : Controller
    {

        IStudentRepo studentRepo;
        IDepartmentRepo deptRepo;

        public StudentController(IStudentRepo _stdRepo, IDepartmentRepo _deptRepo)
        {
            studentRepo = _stdRepo;
            deptRepo = _deptRepo;
        }

        public IActionResult Index()
        {
            return View(studentRepo.GetAllStudents());
        }
      
       public IActionResult InsertNew()
        {
            ViewData["Department"] = deptRepo.GetAll();
            return View();
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveInsertNew(Student obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Dept != 0)
                {
                    studentRepo.SaveInsertNewStudent(obj);
                    return RedirectToAction("Index");
                }

            }
            else
            {
                ModelState.AddModelError("", "there is somthing wrong");
            }
            ViewData["Department"] = deptRepo.GetAll();
            return View("InsertNew", obj);
        }

        public IActionResult Delete(int id)
        {
            var student = studentRepo.GetSpecificStudent(id);
            return View(student);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveDelete(int id)
        {
            if (ModelState.IsValid)
            {
                studentRepo.ConfirmDeleteStudent(id);
                return RedirectToAction("Index");
            }
            return View("Delete",studentRepo.GetSpecificStudent(id));
        }
        public IActionResult Edite(int id)
        {
            ViewData["Department"] = deptRepo.GetAll();
            return View(studentRepo.Edite(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdite(Student obj)
        {
            if (ModelState.IsValid)
            {
                studentRepo.SaveEditedStudent(obj);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "there is somthing wrong");
            }
            ViewData["Department"] = deptRepo.GetAll();
            return View("Edite", obj);
        }
       
    }
}