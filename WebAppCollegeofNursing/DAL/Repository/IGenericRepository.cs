using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
   public interface IGenericRepository<T> 
    {
        int Add(T model);
        IQueryable<T> GetAllIQueryable();
       
        IQueryable<T> GetAll();
        IEnumerable<T> GetAllWithMultipleIncludes(params string[] navProperties);
        T GetById(int? id);
        List<T> FindByWithMultipleIncludes(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] navProperties);
        int Delete(T model);
        int DeleteRange(List<T> obj);
        int Update(T model);
        IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
        int GetCount();
        int InsertList(List<T> obj);
         bool IsExist(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

    }
}
