using System.Collections.Generic;
using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.Business.Abstract.EntityFramework
{
    public interface IModuleService
    {
        Modules GetByModuleID(int id);
        List<Modules> GetByModuleIDList(int id);
        List<Modules> GetByModuleList(int? id);
        void Update(Modules module);
        void Add(Modules module);
        List<Modules> GetList();
        void Delete(Modules module);
    }
}
