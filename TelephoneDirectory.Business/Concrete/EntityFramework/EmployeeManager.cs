using System.Collections.Generic;
using TelephoneDirectory.Business.Abstract.EntityFramework;
using TelephoneDirectory.DataAccess.Abstract.EntityFramework;
using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.Business.Concrete.EntityFramework
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public void Add(Employees employee)
        {
            _employeeDal.Add(employee);
        }

        public void Delete(Employees employee)
        {
            _employeeDal.Delete(employee);
        }

        public Employees GetByDepartmentID(int departmentID)
        {
            return _employeeDal.Find(x => x.DepartmentID == departmentID);
        }

        public List<Employees> GetByDepartmentIDToList(int departmentID)
        {
            return _employeeDal.Query(x => x.DepartmentID == departmentID);
        }

        public Employees GetByEmployeeID(int employeeID)
        {
            return _employeeDal.Find(x => x.EmployeeID == employeeID);
        }

        public Employees GetByFirstName(string firstName)
        {
            return _employeeDal.Find(x => x.FirstName == firstName);
        }

        public Employees GetByManagerID(int managerID)
        {
            return _employeeDal.Find(x => x.ManagerID == managerID);
        }

        public List<Employees> GetByManagerIDToList(int managerID)
        {
            return _employeeDal.Query(x => x.ManagerID == managerID);
        }


        public Employees GetFirstOrDefaultInclude(int id)
        {
            return _employeeDal.GetFirstOrDefaultInclude(x => x.EmployeeID == id, x => x.Employees1, x => x.Employees2, x => x.Departments);
        }

        public List<Employees> GetList()
        {
            return _employeeDal.GetList();
        }

        public List<Employees> TolistInclude()
        {
            return _employeeDal.TolistInclude(x => x.Employees1, x => x.Employees2, x => x.Departments);
        }

        public void Update(Employees employee)
        {
            _employeeDal.Update(employee);
        }
    }
}
