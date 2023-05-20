using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Water_consumption_monitors.DTO
{
    public class TypesOfRealEstateDto
    {
        public int TypesCode { get; set; }
        public string TypesName { get; set; }
        public string TypesNote { get; set; }
    }
}
