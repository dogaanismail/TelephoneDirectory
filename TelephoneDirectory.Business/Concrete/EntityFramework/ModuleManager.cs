using System.Collections.Generic;
using TelephoneDirectory.Business.Abstract.EntityFramework;
using TelephoneDirectory.DataAccess.Abstract.EntityFramework;
using TelephoneDirectory.Entities.EntityFramework;

namespace TelephoneDirectory.Business.Concrete.EntityFramework
{
    public class ModuleManager : IModuleService
    {
        private IModuleDal _moduleDal;
        public ModuleManager(IModuleDal moduleDal)
        {
            _moduleDal = moduleDal;
        }

        public void Add(Modules module)
        {
            _moduleDal.Add(module);
        }

        public void Delete(Modules module)
        {
            _moduleDal.Delete(module);
        }

        public Modules GetByModuleID(int id)
        {
            return _moduleDal.Find(x => x.ID == id);
        }

        public List<Modules> GetByModuleIDList(int id)
        {
            return _moduleDal.Query(x => x.ID == id);
        }

        public List<Modules> GetByModuleList(int? id)
        {
            if (id == null)
            {
                return _moduleDal.GetList();
            }
            else
            {
                return _moduleDal.Query(x => x.ID == id);
            }
        }

        public List<Modules> GetList()
        {
            return _moduleDal.GetList();
        }

        public void Update(Modules module)
        {
            _moduleDal.Update(module);
        }
    }
}
