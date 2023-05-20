using Water_consumption_monitors.Date;
using Water_consumption_monitors.Interface;
using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Services
{
    public class SlidedistributionServicescs : BassRepository
        <Slidedistribution> , ISlidedistribution
    {
        public SlidedistributionServicescs(ApplicationDbContext context) : base (context)
        {
        }
    }
}
