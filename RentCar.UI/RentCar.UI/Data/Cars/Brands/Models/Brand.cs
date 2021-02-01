using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.UI.Data.Cars.Brands.Models
{
    public enum BrandStatus
    {
        Nuevo,
        Viejo
    }

    public class Brand
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Description is too long, the maximum allowed is 100")]
        public string Description { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Status is too long, the maximum allowed is 50")]
        public string Status { get; set; }
    }
}
