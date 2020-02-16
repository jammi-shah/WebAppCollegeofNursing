using DAL.Repository;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
   public class ClassInfoBLL
    {
        IGenericRepository<ClassInfo> _class = new GenericRepository<ClassInfo>();
        public int AddClass(ClassInfo classInfo)
        {
            if(_class.IsExist(x => x.ClassName == classInfo.ClassName))
            {
                return -1;
            }
           return _class.Add(classInfo);
        }
        public int EditClass(ClassInfo classInfo)
        {
            if (_class.IsExist(x => x.ClassName == classInfo.ClassName))
            {
                return -1;
            }
            return _class.Update(classInfo);
        }
        public List<ClassInfo> GetAllClasses()
        {
            return _class.GetAll().ToList();
        }
        public int DeleteClass(int id)
        {
            return _class.Delete(_class.GetById(id));
        }
    }
}
