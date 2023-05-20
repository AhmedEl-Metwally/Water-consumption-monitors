using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Water_consumption_monitors.Models
{
    [Table("Slidedistribution")]
    public partial class Slidedistribution
    {
        [Key]
        [StringLength(10)]
        [Unicode(false)]
        public int SlideNumber { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string SlideDescription { get; set; }
        public int? AmountExpenditureSlide { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? PricePerCubicMeterOfWater { get; set; }
        [Column("priceServiceSewage", TypeName = "decimal(18, 2)")]
        public decimal? PriceServiceSewage { get; set; }
        [StringLength(200)]
        [Unicode(false)]
        public string SlideDistributionNote { get; set; }
        [StringLength(10)]
        [Unicode(false)]
        public int? TypesCode { get; set; }
    }
}
