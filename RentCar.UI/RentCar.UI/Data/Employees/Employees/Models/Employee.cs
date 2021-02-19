using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.UI.Data.Employees.Employees.Models
{
    public enum EmployeeStatus
    {
        Activo,
        Inactivo
    }

    public enum EmployeeHourHand
    {
        Matutina,
        Vespertina,
        Nocturna
    }

    public class Employee
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long, the maximum allowed is 100")]
        public string Name { get; set; }
        [Required]
        [StringLength(11, ErrorMessage = "IdentificationCard is too long, the maximum allowed is 11")]
        public string IdentificationCard { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "HourHand is too long, the maximum allowed is 50")]
        public string HourHand { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "CommisionPercent is too long, the maximum allowed is 50")]
        public string CommisionPercent { get; set; } 
        public DateTime Created { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Status is too long, the maximum allowed is 50")]
        public string Status { get; set; }
    }
}