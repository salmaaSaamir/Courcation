using iti.Models;

namespace iti.Repos
{
    public interface IDepartmentRepo
    {
        List<Department> GetAll();
        List<Department> SaveInsertDept(Department obj);
        Department GetSpecificDept(int id);
        void SaveEditeDept(Department obj);
        void ConfirmDeptToDelete(int id);
    }
}