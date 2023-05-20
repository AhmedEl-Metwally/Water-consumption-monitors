using Water_consumption_monitors.Date;
using Water_consumption_monitors.Interface;
using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Services
{
    public class SubscriptionServicescs : BassRepository
        <Subscription> , ISubscription
    {
        public SubscriptionServicescs (ApplicationDbContext context) : base (context) 
        {
        }
    }
}
