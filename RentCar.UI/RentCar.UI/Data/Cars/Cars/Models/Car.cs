using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.UI.Data.Cars.Cars.Models
{
    public enum CarStatus
    {
        Nuevo,
        Viejo
    }

    public class Car
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Description is too long, the maximum allowed is 100")]
        public string Description { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Status is too long, the maximum allowed is 50")]
        public string Status { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "ChassisNumber is too long, the maximum allowed is 50")]
        public string ChassisNumber { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "EngineNumber is too long, the maximum allowed is 50")]
        public string EngineNumber { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "PlateNumber is too long, the maximum allowed is 50")]
        public string PlateNumber { get; set; }
        [Required]
        public int? BrandID { get; set; }
        public string Brand { get; set; }
        [Required]
        public int? ModelID { get; set; }
        public string Model { get; set; }
        [Required]
        public int? TypeOfCarID { get; set; }
        public string TypeOfCar { get; set; }
        [Required]
        public int? TypeOfFuelID { get; set; }
        public string TypeOfFuel { get; set; }
    }
}
