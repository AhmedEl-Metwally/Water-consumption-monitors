using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Water_consumption_monitors.DTO
{
    public class SubscriptionDto
    {
        public int SubscriptionNumber { get; set; }
        public int? SubscriberNumber { get; set; }
        public int? HouseType { get; set; }
        public int? TheNumberOfFloorsOfTheHouse { get; set; }
        public bool? IsThereSanitation { get; set; }
        public int? TheLastReadingOfTheMeter { get; set; }
        public string SubscriptionNote { get; set; }
    }
}
