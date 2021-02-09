using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.UI.Data.Rents.Models
{
    public enum RentStatus
    {
        Rented,
        Returned
    }

    public class Rent
    {
        public int ID { get; set; }
        public DateTime RentalDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        [Required]
        public int? AmountForDays { get; set; }
        [Required]
        public int? NumberOfDays { get; set; }
        public string Status { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Comment is too long, the maximum allowed is 100")]
        public string Comment { get; set; }
        [Required]
        public int? EmployeeID { get; set; }
        public string Employee { get; set; }
        [Required]
        public int? ClientID { get; set; }
        public string Client { get; set; }
        [Required]
        public int? CarID { get; set; }
        public string Car { get; set; }
    }
}
