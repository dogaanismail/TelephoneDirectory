using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.DataAccess.Abstract.EntityFramework
{
    public interface IEmployeeDal : IEntityRepository<Employees>
    {
    }
}
