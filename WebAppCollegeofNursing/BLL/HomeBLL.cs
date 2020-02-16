
using DAL.Repository;
using ModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class HomeBLL
    {
        IGenericRepository<Slider> _sliderDal = new GenericRepository<Slider>();
        IGenericRepository<Notification> _notificationDal = new GenericRepository<Notification>();
        IGenericRepository<UserAccount> _accountDal = new GenericRepository<UserAccount>();
        IGenericRepository<Event> _eventDal = new GenericRepository<Event>();

        public HomeVM GetDataForHome()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.Faculty = _accountDal.FindBy(x => x.UserRole == "faculty" || x.UserRole == "HOD").Take(6).ToList();
            homeVm.Sliders = _sliderDal.FindBy(x => x.IsActive == true).ToList();
            homeVm.Notifications = _notificationDal.FindBy(x => true).Take(10).ToList();
            homeVm.Events = _eventDal.FindBy(x => x.IsActive == true).OrderByDescending(x => x.Id).ToList();
            return homeVm;
        }
        public Event EventDetails(int id)
        {
            return _eventDal.GetById(id);
        }
        public List<UserAccount> GetAllFaculties()
        {
            return _accountDal.FindBy(x => x.UserRole == "faculty" || x.UserRole=="HOD").ToList();
        }
    }
}
