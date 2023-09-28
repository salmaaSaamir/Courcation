using iti.Models;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace iti.Repos
{
    public class DepartmentRepo:IDepartmentRepo
    {
        Data db;
        public DepartmentRepo(Data _db )
        {
            db = _db;
        }
        public List<Department> GetAll()
        {
            var departments = db.Departments.ToList();
            return departments;
        }
        public List<Department> SaveInsertDept(Department obj)
        {
            db.Departments.Add(obj);
            db.SaveChanges();
            return GetAll();
        }
        public Department GetSpecificDept(int id)
        {
            Department dept = db.Departments.FirstOrDefault(x => x.Id == id);
            return dept;
        }
        public void SaveEditeDept(Department obj)
        {
                db.Departments.Update(obj);
                db.SaveChanges();
        }
        public void ConfirmDeptToDelete(int id)
        {
            var dept = GetSpecificDept(id);
            if (dept != null)
            {
                db.Departments.Remove(dept);
                db.SaveChanges();
            }
        }
    }
}
