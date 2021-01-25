using System;
namespace RentCar.Core.Entities
{
    public class Model
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int BrandID { get; set; }
    }
}