using TelephoneDirectory.DataAccess.Abstract.EntityFramework;
using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.DataAccess.Concrete.EntityFramework
{
    public class EfDepartmentDal : EfEntityRepositoryBase<Departments, TelephoneDirectoryModel>, IDepartmentDal
    {
    }
}
