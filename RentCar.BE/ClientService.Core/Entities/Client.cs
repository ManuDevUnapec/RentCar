using System;
namespace ClientService.Core.Entities
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string IdentificationCard { get; set; }
        public string CardNumber { get; set; }
        public int CreditLimit { get; set; }
        public string PersonType { get; set; }
        public string Status { get; set; }
    }
}