using System;
namespace RentService.Core.Entities
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
        public int AmountForDays { get; set; }
        public int NumberOfDays { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        public int EmployeeID { get; set; }
        public string Employee { get; set; }
        public int ClientID { get; set; }
        public string Client { get; set; }
        public int CarID { get; set; }
        public string Car { get; set; }
    }
}