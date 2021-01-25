using System;
namespace InspectionService.Core.Entities
{
    public class Inspection
    {
        public int ID { get; set; }
        public DateTime InspectionDate { get; set; }
        public bool Grated { get; set; }
        public bool Cat { get; set; }
        public bool RubberBack { get; set; }
        public bool GlassBreak { get; set; }
        public bool RubberStateOne { get; set; }
        public bool RubberStateTwo { get; set; }
        public bool RubberStateThree { get; set; }
        public bool RubberStateFour { get; set; }
        public string AmountOfFuel { get; set; }
        public string Status { get; set; }
        public int EmployeeID { get; set; }
        public string Employee { get; set; }
        public int ClientID { get; set; }
        public string Client { get; set; }
        public int CarID { get; set; }
        public string Car { get; set; }
    }
}
