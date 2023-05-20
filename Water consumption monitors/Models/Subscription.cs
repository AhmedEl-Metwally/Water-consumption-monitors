using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Water_consumption_monitors.Models
{
    public partial class Subscription
    {
        public Subscription()
        {
            Invoices = new HashSet<Invoice>();
        }

        [Key]
        [StringLength(14)]
        [Unicode(false)]
        public int SubscriptionNumber { get; set; }
        [StringLength(14)]
        [Unicode(false)]
        public int? SubscriberNumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public int? HouseType { get; set; }
        public int? TheNumberOfFloorsOfTheHouse { get; set; }
        public bool? IsThereSanitation { get; set; }
        public int? TheLastReadingOfTheMeter { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string SubscriptionNote { get; set; }

        [ForeignKey("HouseType")]
        [InverseProperty("Subscriptions")]
        public virtual TypesOfRealEstate HouseTypeNavigation { get; set; }
        [ForeignKey("SubscriberNumber")]
        [InverseProperty("Subscriptions")]
        public virtual Subscriber SubscriberNumberNavigation { get; set; }
        [InverseProperty("SubscriptionNumberNavigation")]
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
