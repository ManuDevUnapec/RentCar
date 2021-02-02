using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.UI.Data.Cars.Models.Models
{
    public enum ModelStatus
    {
        Nuevo,
        Viejo
    }

    public class Model
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Description is too long, the maximum allowed is 100")]
        public string Description { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Status is too long, the maximum allowed is 50")]
        public string Status { get; set; }
        [Required]
        public int? BrandID { get; set; }
        public string Brand { get; set; }
    }
}