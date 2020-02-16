using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using DAL.Repository;
using ModelLayer;

namespace BLL
{
   public class SliderAndNotificationBLL
    {
        IGenericRepository<Slider> _sliderDal = new GenericRepository<Slider>();
        IGenericRepository<Notification> _notificationDal = new GenericRepository<Notification>();
        IGenericRepository<UserAccount> _accountDal = new GenericRepository<UserAccount>();
        IGenericRepository<Event> _eventDal = new GenericRepository<Event>();

        public List<UserAccount> GetFaculty()
        {
            return _accountDal.FindBy(x => x.UserRole == "Faculty").Take(6).ToList();
        }
        public int AddSlider(Slider slider)
        {
            slider.IsActive = true;
            slider.ImagePath = CommonBLL.FileProfileUpload(slider.File, "~/Images/Slider/");
            return _sliderDal.Add(slider);
        }
        public int EditSlider(Slider slider)
        {
            return _sliderDal.Update(slider);
        }
        public int DeleteSlider(int id)
        {
            Slider slider=_sliderDal.GetById(id);
            System.IO.File.Delete(HttpContext.Current.Server.MapPath(slider.ImagePath));
            return _sliderDal.Delete(slider);
        }
        public int BlockSlider(int id)
        {
            Slider slider = _sliderDal.GetById(id);
            slider.IsActive = !slider.IsActive;
            return _sliderDal.Update(slider);
        }
        public Slider GetSliderById(int id)
        {
           return _sliderDal.FindBy(x => x.Id == id).FirstOrDefault();
        }
        public List<Slider> GetSlider()
        {
            return _sliderDal.FindBy(x => true).ToList();
        }
        public int AddNotification(Notification not)
        {
            return _notificationDal.Add(not);
        }
        public int EditNotification(Notification not)
        {
            return _notificationDal.Update(not);
        }
        public int DeleteNotification(Notification not)
        {
            return _notificationDal.Delete(not);
        }
        public Notification GetNotificationById(int id)
        {
            return _notificationDal.FindBy(x => x.Id == id).FirstOrDefault();
        }
        public List<Notification> GetNotifications()
        {
            return _notificationDal.FindBy(x => true).ToList();
        }
        public int AddEvent(Event events)
        {
            events.ImagePath = CommonBLL.FileProfileUpload(events.File, "~/Images/Events/");
            return _eventDal.Add(events);
        }
        public List<Event> GetEvents()
        {
            return _eventDal.GetAll().ToList();
        }
        public int DeleteEvent(int id)
        {
            Event events = _eventDal.GetById(id);
            System.IO.File.Delete(HttpContext.Current.Server.MapPath(events.ImagePath));
            return _eventDal.Delete(events);
        }
        public int BlockEvent(int id)
        {
            Event events = _eventDal.GetById(id);
            events.IsActive = !events.IsActive;
            return _eventDal.Update(events);
        }
    }
}
