using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class HomeVM
    {
        public List<UserAccount> Faculty { get; set; }
        public List<Notification> Notifications { get; set; }
        public List<Slider> Sliders { get; set; }
        public List<Event> Events { get; set; }
    }
}
