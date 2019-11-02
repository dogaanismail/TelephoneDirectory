using System.Collections.Generic;

namespace TelephoneDirectory.Entities.EntityClasses
{
    public class DashboardData
    {
        public int? TotalUsers { get; set; }
        public int? TotalEmployees { get; set; }
        public int? TotalDepartments { get; set; }
        public int? TotalRoles { get; set; }
        public virtual List<TelephoneDirectory.Entities.EntityClasses.Employee> Employees { get; set; }
        public List<Employee> UsersMapData { get; set; }
    }
}
