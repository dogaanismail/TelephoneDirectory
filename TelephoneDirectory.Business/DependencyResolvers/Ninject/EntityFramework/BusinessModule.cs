using Ninject.Modules;
using TelephoneDirectory.Business.Abstract.EntityFramework;
using TelephoneDirectory.Business.Concrete.EntityFramework;
using TelephoneDirectory.DataAccess.Abstract.EntityFramework;
using TelephoneDirectory.DataAccess.Concrete.EntityFramework;

namespace TelephoneDirectory.Business.DependencyResolvers.Ninject.EntityFramework
{
    public class BusinessModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDepartmentService>().To<DepartmentManager>().InSingletonScope();
            Bind<IDepartmentDal>().To<EfDepartmentDal>().InSingletonScope();

            Bind<IEmployeeService>().To<EmployeeManager>().InSingletonScope();
            Bind<IEmployeeDal>().To<EfEmployeeDal>().InSingletonScope();

            Bind<IModuleService>().To<ModuleManager>().InSingletonScope();
            Bind<IModuleDal>().To<EfModuleDal>().InSingletonScope();
        }
    }
}
