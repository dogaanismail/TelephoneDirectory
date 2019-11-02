using System.Collections.Generic;
using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.Business.Abstract.EntityFramework
{
    public interface IDepartmentService
    {
        Departments GetByDepartmentID(int departmentID);
        List<Departments> GetList();
        void Update(Departments department);
        void Add(Departments department);
        void Delete(Departments department);
        Departments GetFirstOrDefaultInclude(int departmentID);
    }
}
