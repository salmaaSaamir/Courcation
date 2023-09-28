using iti.Controllers;
using iti.Models;
using iti.ViewModelClasses;
using Microsoft.EntityFrameworkCore;
using System.Reflection.PortableExecutable;

namespace iti.Repos
{
    public class StudentRepo:IStudentRepo
    {
        Data db;
        IWebHostEnvironment webHostEnvironment;
        AccountController accountController;
        public StudentRepo(Data _db, IWebHostEnvironment webHostEnvironment ,AccountController accountController)
        {
            db = _db;
            this.webHostEnvironment = webHostEnvironment;
            this.accountController = accountController;
        }


        public List<StudentWithDepartmentcs> GetAllStudents()
        {
            var students = db.Students.Include("Department");
            List<StudentWithDepartmentcs> studentsWithDepartments = new List<StudentWithDepartmentcs>();

            foreach (var item in students)
            {
                StudentWithDepartmentcs studentWithDept = new StudentWithDepartmentcs()
                {
                    Id = item.Id,
                    studentName = item.Name,
                    code = item.Code,
                    studentAge = item.Age,
                    studentAddress = item.Address,
                    deptName = item.Department.Name,
                    Image = item.Image,
                    studentGpa = item.Gpa,
                    studentLevel = item.Level,
                };

                if (item.Gpa < 2.5)
                {
                    studentWithDept.color = "#fdbdc4";

                }
                else if (item.Gpa > 3.5)
                {
                    studentWithDept.award = true;
                }

                studentsWithDepartments.Add(studentWithDept);
            }
            return studentsWithDepartments;
        }
        public Student GetSpecificStudent(int id )
        {
            return db.Students.Include("Department").FirstOrDefault(x => x.Id == id);
        }

        public void SaveInsertNewStudent(Student obj)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.ImageFile.FileName;
            if (obj.ImageFile != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                obj.ImageFile.CopyTo(new FileStream(filePath,FileMode.Create));

            }
            obj.Image = uniqueFileName;
            db.Students.Add(obj);
            db.SaveChanges();
        }
        public void ConfirmDeleteStudent(int id)
        {
            var student = GetSpecificStudent(id);
            db.Students.Remove(student);
            db.SaveChanges();
        }
        public Student Edite(int id)
        {
            var student = db.Students.Include("Department").FirstOrDefault(x => x.Id == id);
            return student;
            
        }
        public void SaveEditedStudent(Student obj)
        {
            if (obj.ImageFile != null)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + obj.ImageFile.FileName;
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                // Detach the previously tracked entity
                var oldOne = db.Students.Local.FirstOrDefault(x => x.Id == obj.Id);
                if (oldOne != null)
                {
                    db.Entry(oldOne).State = EntityState.Detached;
                }

                string fullOldPath = Path.Combine(uploadsFolder, obj.Image);

                if (filePath != fullOldPath)
                {
                    File.Delete(fullOldPath);
                    obj.ImageFile.CopyTo(new FileStream(filePath, FileMode.Create));
                    obj.Image = uniqueFileName;
                }
            }
            else
            {
                // Retain the existing image
                var existingStudent = db.Students.AsNoTracking().FirstOrDefault(x => x.Id == obj.Id);
                if (existingStudent != null)
                {
                    obj.Image = existingStudent.Image;
                }
            }

            db.Students.Update(obj);
            db.SaveChanges();
        }
    }
}
