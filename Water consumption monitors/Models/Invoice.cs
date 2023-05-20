using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Water_consumption_monitors.Models
{
    public partial class Invoice
    {
        [Key]
        [StringLength(10)]
        [Unicode(false)]
        public int InvoiceNumber { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public string FiscalYear { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public int? HouseType { get; set; }
        [StringLength(14)]
        [Unicode(false)]
        public int? SubscriptionNumber { get; set; }
        [StringLength(14)]
        [Unicode(false)]
        public int? SubscriberNumber { get; set; }
        [Column(TypeName = "date")]
        public DateTime? InvoiceDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FromTheDateOf { get; set; }
        [Column(TypeName = "date")]
        public DateTime? FromTheDateTo { get; set; }
        public int? PreviousConsumptionAmount { get; set; }
        public int? CurrentConsumptionAmount { get; set; }
        public int? AmountOfConsumption { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ServiceFee { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TaxFee { get; set; }
        public bool? SanitationIsAvailable { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TheValueOfWaterConsumption { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? WasteWaterConsumptionValue { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalInvoice { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TaxValue { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalBill { get; set; }
        [StringLength(500)]
        [Unicode(false)]
        public string InvoicesNote { get; set; }

        [ForeignKey("HouseType")]
        [InverseProperty("Invoices")]
        public virtual TypesOfRealEstate HouseTypeNavigation { get; set; }
        [ForeignKey("SubscriberNumber")]
        [InverseProperty("Invoices")]
        public virtual Subscriber SubscriberNumberNavigation { get; set; }
        [ForeignKey("SubscriptionNumber")]
        [InverseProperty("Invoices")]
        public virtual Subscription SubscriptionNumberNavigation { get; set; }
    }
}
