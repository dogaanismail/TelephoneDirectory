using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using TelephoneDirectory.Business.Abstract.EntityFramework;
using TelephoneDirectory.Business.DependencyResolvers.Ninject.EntityFramework;
using TelephoneDirectory.Entities.EntityClasses;
using TelephoneDirectory.Entities.EntityFramework;
using TelephoneDirectory.WebApp.Models;

namespace TelephoneDirectory.WebApp.Areas.Admin.Controllers
{
    #region Dashboard api
    [Authorize(Roles = "Admin")]
    public class DashboardDataController : ApiController
    {
        private readonly UserManager<User> userManager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
        private IEmployeeService _employeeService = InstanceFactory.GetInstance<IEmployeeService>();
        private IDepartmentService _departmentService = InstanceFactory.GetInstance<IDepartmentService>();
        private RoleManager<Roles> roleManager = new RoleManager<Roles>(new RoleStore<Roles>(new ApplicationDbContext()));
        public DashboardData Get()
        {
            DashboardData objdashboarddata = new DashboardData
            {
                //Get Total Users Count
                TotalUsers = userManager.Users.ToList().Count(),

                //Get Total Employees Count
                TotalEmployees = _employeeService.GetList().ToList().Count(),

                //Get Total Departments Count
                TotalDepartments = _departmentService.GetList().ToList().Count(),

                //Get Current Months Register Users
                TotalRoles = roleManager.Roles.ToList().Count()
            };
            return objdashboarddata;
        }
    }
    #endregion

    #region RoleApi
    [Authorize(Roles = "Admin")]
    public class RolesController : ApiController
    {
        private RoleManager<Roles> roleManager = new RoleManager<Roles>(new RoleStore<Roles>(new ApplicationDbContext()));
        private readonly UserManager<User> userManager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        //Roles get api
        public IEnumerable<Entities.EntityClasses.Role> Get()
        {                      
            List<Entities.EntityClasses.Role> role = new List<TelephoneDirectory.Entities.EntityClasses.Role>();
            role = roleManager.Roles.ToList().Select(x => new Entities.EntityClasses.Role
            {
                RoleID = x.Id,
                RoleName = x.Name
            }).ToList();
            return role;
        }

        //Roles insert/update api
        public IHttpActionResult Post(Entities.EntityClasses.Role role)
        {
            try
            {
                if (role.RoleID == "")
                {
                    if (roleManager.Roles.Any(x => x.Name == role.RoleName))
                    {
                        res["success"] = 0;
                        res["message"] = "Role is already exist.";
                    }
                    else
                    {
                        Roles newRole = new Roles
                        {
                            Name = role.RoleName
                        };
                        roleManager.Create(newRole);
                        res["success"] = 1;
                        res["message"] = "Role has been created successfully.";
                    }
                }
                else
                {
                    if (roleManager.Roles.Any(x => x.Name == role.RoleName && x.Id != role.RoleID))
                    {
                        res["success"] = 0;
                        res["message"] = "Role is already exist.";
                    }
                    else
                    {
                        Roles objrole = roleManager.FindById(role.RoleID);
                        objrole.Id = role.RoleID;
                        objrole.Name = role.RoleName;
                        roleManager.Update(objrole);
                        res["success"] = 1;
                        res["message"] = "Role has been updated Successfully.";
                    }

                }

            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

        //Role delete by id api
        public IHttpActionResult Delete(string id)
        {
            Roles getRole = roleManager.FindById(id);           
            try
            {
                if (userManager.Users.Any(x=> x.Roles.Any(y=>y.RoleId==id)))
                {
                    res["success"] = 0;
                    res["message"] = "Role can not be deleted, it has reference to another data !";
                }
                else
                {

                    if (getRole != null)
                    {
                        roleManager.Delete(getRole);
                        res["success"] = 1;
                        res["message"] = "Role has been deleted successfully !";
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }
    }

    #endregion

    #region Module api
    [Authorize(Roles = "Admin")]
    public class ModulesController : ApiController
    {
        private IModuleService _moduleService = InstanceFactory.GetInstance<IModuleService>();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        //Get module api
        public IEnumerable<Module> Get()
        {
            List<Module> module = new List<Module>();
            module = _moduleService.GetList().Select(x => new Module()
            {
                ID = x.ID,
                ModuleName= x.ModuleName,
                DisplayOrder=x.DisplayOrder,
                PageIcon=x.PageIcon,
                PageUrl=x.PageUrl,
                ParentModuleID=x.ParentModuleID              
            }).ToList();
            return module;
        }

        //insert/update module api
        public IHttpActionResult Post(Modules module)
        {
            try
            {
                if (module.ID == 0)
                {
                    if (_moduleService.GetList().Any(x => x.ModuleName == module.ModuleName))
                    {
                        res["success"] = 0;
                        res["message"] = "Module is already exist !";
                    }
                    else
                    {
                        _moduleService.Add(module);
                        res["success"] = 1;
                        res["message"] = "Module has been created successfully !";
                    }
                }
                else
                {
                    if (_moduleService.GetList().Any(x => x.ModuleName == module.ModuleName && x.ID != module.ID))
                    {
                        res["success"] = 0;
                        res["message"] = "Module is already exist !";
                    }
                    else
                    {
                       Modules updateModule = _moduleService.GetByModuleID(module.ID);
                        updateModule.DisplayOrder = module.DisplayOrder;
                        updateModule.ModuleName = module.ModuleName;
                        updateModule.PageIcon = module.PageIcon;
                        updateModule.PageUrl = module.PageUrl;
                        updateModule.ParentModuleID = module.ParentModuleID;
                        _moduleService.Update(updateModule);
                        res["success"] = 1;
                        res["message"] = "Module has been updated Successfully !";
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }
            return Ok(res);
        }

        //delete module by id api
        public IHttpActionResult Delete(int id)
        {
            try
            {
                   Modules module = _moduleService.GetByModuleID(id);
                    if (module != null)
                    {
                        _moduleService.Delete(module);
                        res["success"] = 1;
                        res["message"] = "Module has been deleted successfully !";
                    }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }
    }
    #endregion

    #region User api
    [Authorize(Roles = "Admin")]
    public class UsersController : ApiController
    {        
        private RoleManager<Roles> roleManager = new RoleManager<Roles>(new RoleStore<Roles>(new ApplicationDbContext()));
        private readonly UserManager<User> userManager = new UserManager<User>(new UserStore<User>(new ApplicationDbContext()));
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();      
        //Users get api
        public IEnumerable<ApplicationUser> Get()
        {
            List<ApplicationUser> user = new List<ApplicationUser>();
            user = userManager.Users.ToList().Select(x => new ApplicationUser
            {
                Id = x.Id,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                PasswordHash = x.PasswordHash,
                SecurityStamp = x.SecurityStamp,
                PhoneNumber = x.PhoneNumber,
                PhoneNumberConfirmed = x.PhoneNumberConfirmed,
                TwoFactorEnabled = x.TwoFactorEnabled,
                LockoutEndDateUtc = x.LockoutEndDateUtc,
                LockoutEnabled = x.LockoutEnabled,
                AccessFailedCount = x.AccessFailedCount,
                UserName = x.UserName,
                RoleID =   x.Roles.Count() > 0  ? x.Roles.FirstOrDefault().RoleId.ToString() : ""
            }).ToList();
            return user;
        }

        //User insert/update api
        public IHttpActionResult Post(ApplicationUser user)
        {
            Models.User getUser = userManager.FindById(user.Id);
            try
            {
                if (getUser==null)
                {
                    if (userManager.Users.Any(x => x.UserName == user.UserName))
                    {
                        res["success"] = 0;
                        res["message"] = "Username is already exist !";
                    }
                    else
                    {
                        Models.User newUser = new User
                        {
                            Email = user.Email,
                            PhoneNumber = user.PhoneNumber,
                            UserName = user.UserName
                        };
                     
                        var role = roleManager.FindById(user.RoleID);
                        UserRole getRole = new UserRole
                        {
                            UserId = newUser.Id,
                            RoleId = role.Id
                        };
                        newUser.Roles.Add(getRole);
                        var result = userManager.Create(newUser, user.PasswordHash);
                        if (result.Succeeded)
                        {
                            res["success"] = 1;
                            res["message"] = "User has been created successfully !";
                        }
                        else
                        {
                            res["success"] = 0;
                            res["message"] = result.Errors.ToString();
                        }
                    }
                }
                else
                {
                        getUser.Email = user.Email;
                        getUser.EmailConfirmed = user.EmailConfirmed;
                        getUser.PasswordHash = user.PasswordHash;
                        getUser.SecurityStamp = user.SecurityStamp;
                        getUser.PhoneNumber = user.PhoneNumber;
                        getUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
                        getUser.TwoFactorEnabled = user.TwoFactorEnabled;
                        getUser.LockoutEndDateUtc = user.LockoutEndDateUtc;
                        getUser.LockoutEnabled = user.LockoutEnabled;
                        getUser.AccessFailedCount = user.AccessFailedCount;
                        getUser.UserName = user.UserName;
                        var updateResult = userManager.Update(getUser);
                        if (updateResult.Succeeded)
                        {

                            res["success"] = 1;
                            res["message"] = "User has been updated successfully !";
                        }
                        else
                        {
                            res["success"] = 0;
                            res["message"] = updateResult.Errors.ToString();
                        }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

        //User delete by id api
        public IHttpActionResult Delete(string id)
        {
            Models.User getUser = userManager.FindById(id);
            try
            {
                if (getUser.Roles.Count() > 0)
                {
                    res["success"] = 0;
                    res["message"] = "User can not be deleted, it has reference to another data !";
                }
                else
                {
                    if (getUser != null)
                    {
                        var result = userManager.Delete(getUser);
                        if (result.Succeeded)
                        {
                            res["success"] = 1;
                            res["message"] = "User has been deleted successfully !";
                        }
                        else
                        {
                            res["success"] = 0;
                            res["message"] = result.Errors.ToString();
                        }
                        
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }
    }
    #endregion

    #region DepartmentApi
    [Authorize(Roles = "Admin")]
    public class DepartmentsController : ApiController
    {
        private IDepartmentService _departmentService = InstanceFactory.GetInstance<IDepartmentService>();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        //Get department api
        public IEnumerable<Department> Get()
        {
            List<Department> department = new List<Department>();
            department = _departmentService.GetList().Select(x => new Department()
            {
                DepartmentID = x.DepartmentID,
                DepartmentName = x.DepartmentName,
                ManagerID = x.ManagerID,          
            }).ToList();
            return department;
        }

        //insert/update department api
        public IHttpActionResult Post(Departments department)
        {
            try
            {
                if (department.DepartmentID == 0)
                {
                    if (_departmentService.GetList().Any(x => x.DepartmentName == department.DepartmentName))
                    {
                        res["success"] = 0;
                        res["message"] = "Department is already exist !";
                    }
                    else
                    {
                        _departmentService.Add(department);
                        res["success"] = 1;
                        res["message"] = "Department has been created successfully !";
                    }
                }
                else
                {
                    if (_departmentService.GetList().Any(x => x.DepartmentName == department.DepartmentName && x.DepartmentID != department.DepartmentID))
                    {
                        res["success"] = 0;
                        res["message"] = "Department is already exist !";
                    }
                    else
                    {
                        Departments updateDepartment = _departmentService.GetByDepartmentID(department.DepartmentID);
                        updateDepartment.DepartmentName = department.DepartmentName;
                        updateDepartment.ManagerID = department.ManagerID;        
                        _departmentService.Update(department);
                        res["success"] = 1;
                        res["message"] = "Department has been updated successfully !";
                    }
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }
            return Ok(res);
        }

        //delete department by id api
        public IHttpActionResult Delete(int id)
        {
            Departments department = _departmentService.GetFirstOrDefaultInclude(id);
            Departments deleteDepartment = _departmentService.GetByDepartmentID(id);
            try
            {             
                if (department != null)
                {
                    if ( department.Employees !=null  && department.Employees.Count() > 0)
                    {
                        res["success"] = 0;
                        res["message"] = "Department can not be deleted because it has employees !";
                    }
                    else
                    {
                        _departmentService.Delete(deleteDepartment);
                        res["success"] = 1;
                        res["message"] = "Department has been deleted successfully !";
                    }
                
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }

    }

    #endregion

    #region EmployeeApi
    [Authorize(Roles = "Admin")]
    public class EmployeesController : ApiController
    {
        private IEmployeeService _employeeService = InstanceFactory.GetInstance<IEmployeeService>();
        private readonly Dictionary<string, object> res = new Dictionary<string, object>();

        //Get employee api
        public IEnumerable<Employee> Get()
        {
            List<Employee> employee = new List<Employee>();
            employee = _employeeService.TolistInclude().Select(x => new Employee()
            {
                EmployeeID = x.EmployeeID,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Phone_Number = x.Phone_Number,
                ManagerID = x.ManagerID,
                DepartmentID = x.DepartmentID,
                DepartmentName = x.Departments != null ? x.Departments.DepartmentName : "" ,
                ManagerName = x.Employees2 !=null ? x.Employees2.FirstName:""
                
            }).ToList();
            return employee;
        }

        //insert/update employee api
        public IHttpActionResult Post(Employees employees)
        {
            Employees updateEmployee = _employeeService.GetByEmployeeID(employees.EmployeeID);
            try
            {              
                if (updateEmployee==null)
                {
                    _employeeService.Add(employees);
                    res["success"] = 1;
                    res["message"] = "Employee has been added successfully.";
                }
                else
                {
                    updateEmployee.EmployeeID = employees.EmployeeID;
                    updateEmployee.FirstName = employees.FirstName;
                    updateEmployee.LastName = employees.LastName;
                    updateEmployee.Phone_Number = employees.Phone_Number;
                    updateEmployee.ManagerID = employees.ManagerID;
                    updateEmployee.DepartmentID = employees.DepartmentID;
                    _employeeService.Update(updateEmployee);
                    res["success"] = 1;
                    res["message"] = "Employee has been updated successfully.";
                }
                      
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }
            return Ok(res);
        }

        //delete employee by id api
        public IHttpActionResult Delete(int id)
        {
            Employees employee = _employeeService.GetFirstOrDefaultInclude(id);
            Employees employees = _employeeService.GetByEmployeeID(id);
            try
            {           
                if (employee != null)
                {
                    if ( employee.Employees2 !=null )
                    {
                        res["success"] = 0;
                        res["message"] = "Employee could not be deleted because it has managers ! ";
                    }
                    else
                    {
                        _employeeService.Delete(employees);
                        res["success"] = 1;
                        res["message"] = "Employee has been deleted successfully !";
                    }
                   
                }
            }
            catch (Exception ex)
            {
                res["success"] = 0;
                res["message"] = ex.Message.ToString();
            }

            return Ok(res);
        }
    }
    #endregion

    #region Menu api
    [Authorize(Roles = "Admin")]
    public class MenuController : ApiController
    {
        private IModuleService _moduleService = InstanceFactory.GetInstance<IModuleService>();
        //Get All Modules
        public IEnumerable<Module> Get()
        {
            List<Module> module = new List<Module>();
            module = _moduleService.GetList().Select(x => new Module()
            {
                ID = x.ID,
                ModuleName = x.ModuleName,
                DisplayOrder = x.DisplayOrder,
                PageIcon = x.PageIcon,
                PageUrl = x.PageUrl,
                ParentModuleID = x.ParentModuleID
            }).OrderBy(x => x.ID).ToList();
            return module;
        }
    }
    #endregion

    

}
