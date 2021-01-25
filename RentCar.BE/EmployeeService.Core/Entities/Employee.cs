using System;
namespace EmployeeService.Core.Entities
{
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string IdentificationCard { get; set; }
        public string HourHand { get; set; }
        public string CommisionPercent { get; set; }
        public DateTime Created { get; set; }
        public string Status { get; set; }
    }
}