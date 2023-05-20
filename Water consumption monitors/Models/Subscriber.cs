using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Water_consumption_monitors.Models
{
    [Table("Subscriber")]
    public partial class Subscriber
    {
        public Subscriber()
        {
            Invoices = new HashSet<Invoice>();
            Subscriptions = new HashSet<Subscription>();
        }

        [Key]
        [StringLength(14)]
        [Unicode(false)]
        public int SubscriberIdentityNumber { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string SubscriberName { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SubscriberGovernorate { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SubscriberArea { get; set; }
        [StringLength(10)]
        public string SubscriberPhoneNumber { get; set; }
        [StringLength(10)]
        public string SubscriberNote { get; set; }

        [InverseProperty("SubscriberNumberNavigation")]
        public virtual ICollection<Invoice> Invoices { get; set; }
        [InverseProperty("SubscriberNumberNavigation")]
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
