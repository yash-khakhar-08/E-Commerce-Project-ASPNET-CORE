using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MarketMatrix_Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }

        [ValidateNever]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public ApplicationUser User { get; set; }

        [ValidateNever]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        [ValidateNever]
        public Products Products { get; set; }

        public int Quantity { get; set; }

        public int total { get; set; }

        public DateTime DateTime { get; set; }

        public string PaymentMode { get; set; }

        public string status { get; set; }

        [ValidateNever]
        public DateTime StatusChangeDate { get; set; }
    }
}
