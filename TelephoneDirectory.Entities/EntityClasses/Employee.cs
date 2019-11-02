namespace TelephoneDirectory.Entities.EntityClasses
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone_Number { get; set; }
        public int? ManagerID { get; set; }
        public int? DepartmentID { get; set; }

        //DepartmentName
        public string DepartmentName { get; set; }

        //ManagerName
        public string ManagerName { get; set; }

    }
}
