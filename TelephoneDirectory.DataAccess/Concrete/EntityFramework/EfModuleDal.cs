using TelephoneDirectory.DataAccess.Abstract.EntityFramework;
using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.DataAccess.Concrete.EntityFramework
{
    public class EfModuleDal : EfEntityRepositoryBase<Modules, TelephoneDirectoryModel>, IModuleDal
    {
    }
}
