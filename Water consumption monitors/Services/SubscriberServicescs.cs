using Water_consumption_monitors.Date;
using Water_consumption_monitors.Interface;
using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Services
{
    public class SubscriberServicescs : BassRepository<Subscriber> , ISubscriber
    {
        public SubscriberServicescs (ApplicationDbContext context) : base (context) 
        {
        }
    }
}
