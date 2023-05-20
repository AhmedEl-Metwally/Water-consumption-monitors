using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Water_consumption_monitors.DTO
{
    public class SubscriberDto
    {
        public int SubscriberIdentityNumber { get; set; }
        public string SubscriberName { get; set; }
        public string SubscriberGovernorate { get; set; }
        public string SubscriberArea { get; set; }
        public string SubscriberPhoneNumber { get; set; }
        public string SubscriberNote { get; set; }
    }
}
