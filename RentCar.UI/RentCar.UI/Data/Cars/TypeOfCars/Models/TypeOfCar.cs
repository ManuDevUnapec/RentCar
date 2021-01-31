using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.UI.Data.Cars.TypeOfCars.Models
{
    public enum TypeOfCarStauts
    {
        Nuevo,
        Viejo
    }

    public class TypeOfCar
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long, the maximum allowed is 100")]
        public string Description { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Status is too long, the maximum allowed is 50")]
        public string Status { get; set; }
    }
}