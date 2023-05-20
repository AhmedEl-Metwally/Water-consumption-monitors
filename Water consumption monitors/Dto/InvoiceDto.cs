using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Water_consumption_monitors.DTO
{
    public class InvoiceDto
    {
        public int InvoiceNumber { get; set; }
        public string FiscalYear { get; set; }
        public int? SubscriberNumber { get; set; }
        public DateTime? InvoiceDate { get; set; }
        public DateTime? FromTheDateOf { get; set; }
        public DateTime? FromTheDateTo { get; set; }
        public int? PreviousConsumptionAmount { get; set; }
        public int? CurrentConsumptionAmount { get; set; }
        public int? AmountOfConsumption { get; set; }
        public decimal? ServiceFee { get; set; }
        public decimal? TaxFee { get; set; }
        public bool? SanitationIsAvailable { get; set; }
        public decimal? TheValueOfWaterConsumption { get; set; }
        public decimal? WasteWaterConsumptionValue { get; set; }
        public decimal? TotalInvoice { get; set; }
        public decimal? TaxValue { get; set; }
        public decimal? TotalBill { get; set; }
        public string InvoicesNote { get; set; }
    }
}
