using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketMatrix_Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BlockNo { get; set; }

        [Required]
        public string ApartmentName { get; set; }

        [Required]
        public string Pincode { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string Country { get; set; }

        public Address(string BlockNo, string ApartmentName, string Pincode, string City, string State, string Country)
        {
            this.BlockNo= BlockNo;
            this.ApartmentName= ApartmentName;
            this.Pincode= Pincode;
            this.City= City;
            this.State= State;
            this.Country= Country;
        }

        public override string ToString()
        {
            return $"{BlockNo}, {ApartmentName}, {Pincode}, {City}, {State}, {Country}";        
        }

    }
}
