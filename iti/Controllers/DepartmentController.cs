using iti.Models;
using iti.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace iti.Controllers
{
    [Authorize]
    public class DepartmentController : Controller
    {
        IDepartmentRepo deptRepo;
        public DepartmentController(IDepartmentRepo _deptRepo)
        {
            deptRepo = _deptRepo;
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(deptRepo.GetAll());
        }
        public IActionResult InsertDept()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveInsertDept(Department obj)
        {
            if(ModelState.IsValid)
            {
                deptRepo.SaveInsertDept(obj);
                return RedirectToAction("Index");
            }
            return View("InsertDept",obj);
         
        }
        public IActionResult Edite(int id)
        {
            return View(deptRepo.GetSpecificDept(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveEdite(Department obj)
        {
            if (ModelState.IsValid)
            {
                deptRepo.SaveEditeDept(obj);
                return RedirectToAction("Index");
            }
            return View("Edite", obj);
        }
        public IActionResult Delete(int id)
        {
            return View(deptRepo.GetSpecificDept(id));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SaveDelete(int id)
        {
            if (ModelState.IsValid)
            {
                deptRepo.ConfirmDeptToDelete(id);
                return RedirectToAction("Index");
            }
            return View("Delete", deptRepo.GetSpecificDept(id));
        }
    }
}