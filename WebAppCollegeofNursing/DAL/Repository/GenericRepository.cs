using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        NursingCollegeDBEntities db = new NursingCollegeDBEntities();

        public IQueryable<T> GetAllIQueryable()
        {
            return db.Set<T>();
        }
        public IQueryable<T> GetAll()
        {
           // db.Configuration.ProxyCreationEnabled = false;
            return db.Set<T>();

        }


        public IEnumerable<T> GetAllWithMultipleIncludes(params string[] navProperties)
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.Set<T>().IncludeMultipleNavProperties<T>(navProperties).ToList();

        }
        public T GetById(int? id)
        {

            return db.Set<T>().Find(id);
        }

        public int Add(T model)
        {
            db.Configuration.ProxyCreationEnabled = false;
            db.Set<T>().Add(model);
            return db.SaveChanges();

        }
        public int GetCount()
        {
            return db.Set<T>().Count();

        }
        public int Delete(T model)
        {

            db.Entry<T>(model).State = EntityState.Deleted;
            return db.SaveChanges();

        }

        public int DeleteRange(List<T> obj)
        {
            //db.Entry<List<T>>(obj).State = EntityState.Deleted;
            db.Set<T>().RemoveRange(obj);
            return db.SaveChanges();

        }
        public int Update(T model)
        {
            
            db.Entry(model).State=EntityState.Modified;
            return db.SaveChanges();


        }
        public IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            //db.Configuration.ProxyCreationEnabled = false;
            return db.Set<T>().Where(predicate);
        }


        public List<T> FindByWithMultipleIncludes(System.Linq.Expressions.Expression<Func<T, bool>> predicate, params string[] navProperties)
        {

            return db.Set<T>().IncludeMultipleNavProperties(navProperties).Where(predicate).ToList();


        }

        public bool IsExist(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return db.Set<T>().Any(predicate);
        }
        public int InsertList(List<T> obj)
        {
            db.Set<T>().AddRange(obj);
            return db.SaveChanges();

        }

    }

    public static class CustomInclude
    {
        public static IQueryable<T> IncludeMultipleNavProperties<T>(this IQueryable<T> query, params string[] navProperties)
        {
            foreach (var navProperty in navProperties)
                query = query.Include(navProperty);
            return query;
        }
    }
}
