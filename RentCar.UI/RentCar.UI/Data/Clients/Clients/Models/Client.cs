using System;
using System.ComponentModel.DataAnnotations;

namespace RentCar.UI.Data.Clients.Clients.Models
{
    public enum ClientStatus
    {
        Activo,
        Inactivo
    }

    public enum ClientPersonType
    {
        Fisica,
        Juridica
    }

    public class Client
    {
        public int ID { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Name is too long, the maximum allowed is 100")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "IdentificationCard is too long, the maximum allowed is 50")]
        public string IdentificationCard { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "CardNumber is too long, the maximum allowed is 50")]
        public string CardNumber { get; set; }
        [Required]
        public int? CreditLimit { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long, the maximum allowed is 50")]
        public string PersonType { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Name is too long, the maximum allowed is 50")]
        public string Status { get; set; }
    }
}