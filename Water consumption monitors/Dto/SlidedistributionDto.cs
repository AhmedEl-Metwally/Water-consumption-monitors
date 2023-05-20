using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Water_consumption_monitors.DTO
{
    public class SlidedistributionDto
    {
        public int SlideNumber { get; set; }
        public string SlideDescription { get; set; }
        public int? AmountExpenditureSlide { get; set; }
        public decimal? PricePerCubicMeterOfWater { get; set; }
        public decimal? PriceServiceSewage { get; set; }
        public string SlideDistributionNote { get; set; }
        public int? TypesCode { get; set; }
    }
}
