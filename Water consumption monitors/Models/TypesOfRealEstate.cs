using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Water_consumption_monitors.Models
{
    [Table("TypesOfRealEstate")]
    public partial class TypesOfRealEstate
    {
        public TypesOfRealEstate()
        {
            Invoices = new HashSet<Invoice>();
            Subscriptions = new HashSet<Subscription>();
        }

        [Key]
        [StringLength(10)]
        [Unicode(false)]
        public int TypesCode { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string TypesName { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string TypesNote { get; set; }

        [InverseProperty("HouseTypeNavigation")]
        public virtual ICollection<Invoice> Invoices { get; set; }
        [InverseProperty("HouseTypeNavigation")]
        public virtual ICollection<Subscription> Subscriptions { get; set; }
    }
}
