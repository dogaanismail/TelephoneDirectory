using System;
using System.Collections.Generic;
using TelephoneDirectory.Business.Abstract.EntityFramework;
using TelephoneDirectory.DataAccess.Abstract.EntityFramework;
using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.Business.Concrete.EntityFramework
{
    public class DepartmentManager : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;

        public DepartmentManager(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public void Add(Departments department)
        {
            _departmentDal.Add(department);
        }

        public void Delete(Departments department)
        {
            _departmentDal.Delete(department);
        }

        public Departments GetByDepartmentID(int departmentID)
        {
            return _departmentDal.Find(x => x.DepartmentID == departmentID);
        }

        public Departments GetFirstOrDefaultInclude(int departmentID)
        {
            return _departmentDal.GetFirstOrDefaultInclude(x => x.DepartmentID == departmentID, x => x.Employees);
        }

        public List<Departments> GetList()
        {
            return _departmentDal.GetList();
        }

        public void Update(Departments department)
        {
            _departmentDal.Update(department);
        }
    }
}
