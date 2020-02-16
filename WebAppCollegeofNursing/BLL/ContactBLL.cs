using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using DAL.Repository;
namespace BLL
{
   public class ContactBLL
    {
        IGenericRepository<Contact> _contactDal = new GenericRepository<Contact>();

        public int SendFeedback(Contact contact)
        {
            return _contactDal.Add(contact);
        }
        public int DeleteFeedback(Contact contact)
        {
            return _contactDal.Delete(contact);
        }
        public List<Contact> GetFeedback()
        {
            return _contactDal.GetAll().ToList();
        }
    }
}
