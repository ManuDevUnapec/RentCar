using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.UI.Data.Inspections.Models
{
    public enum InspectionStatus
    {
        Checking,
        Checked,
        NoChecked
    }

    public enum InspectionAmountOfFuel
    {
        Quarter,
        AMedium,
        ThreeFour,
        Full
    }

    public class Inspection
    {
        public int ID { get; set; }
        public DateTime InspectionDate { get; set; }
        [Required]
        public bool Grated { get; set; }
        [Required]
        public bool Cat { get; set; }
        [Required]
        public bool RubberBack { get; set; }
        [Required]
        public bool GlassBreak { get; set; }
        [Required]
        public bool RubberStateOne { get; set; }
        [Required]
        public bool RubberStateTwo { get; set; }
        [Required]
        public bool RubberStateThree { get; set; }
        [Required]
        public bool RubberStateFour { get; set; }
        [Required]
        public string AmountOfFuel { get; set; }
        [Required]
        public string Status { get; set; }
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
