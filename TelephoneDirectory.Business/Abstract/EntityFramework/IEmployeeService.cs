using System.Collections.Generic;
using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.Business.Abstract.EntityFramework
{
    public interface IEmployeeService
    {
        Employees GetByEmployeeID(int employeeID);
        Employees GetByFirstName(string firstName);
        Employees GetByDepartmentID(int departmentID);
        Employees GetByManagerID(int managerID);
        Employees GetFirstOrDefaultInclude(int id);

        List<Employees> GetList();
        List<Employees> TolistInclude();
        List<Employees> GetByDepartmentIDToList(int departmentID);
        List<Employees> GetByManagerIDToList(int managerID);

        void Update(Employees employee);
        void Add(Employees employee);
        void Delete(Employees employee);
      
    }
}
